using Dapper;
using HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace HRMS.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        private readonly LanguageService _languageService;
        private readonly IUserService _userService;

        public DatabaseService(IConfiguration configuration, LanguageService languageService, IUserService userService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _languageService = languageService;
            _userService = userService;
        }

        public async Task<DbResponse<T>> ExecuteQueryAsync<T>(string procedureName, object? jsonParams = null, int? employeeId = null, string? language = null, int? roleId = null, bool useTransaction = false)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                return new DbResponse<T> { Success = -1, Message = "Connection string not configured." };
            }

            try
            {
                using var db = new SqlConnection(_connectionString);
                await db.OpenAsync();

                var lang = language ?? _languageService.CurrentLanguage;
                var finalEmployeeId = employeeId ?? _userService.CurrentUser?.EmployeeId ?? 1;
                var finalRoleId = roleId ?? _userService.CurrentRole?.RoleId ?? 0;
                var jsonInput = jsonParams != null ? JsonSerializer.Serialize(jsonParams) : "{}";

                SqlTransaction? transaction = useTransaction ? (SqlTransaction)db.BeginTransaction() : null;
                
                try
                {
                    var jsonResult = await db.QueryFirstOrDefaultAsync<string>(procedureName, 
                        new { EmployeeId = finalEmployeeId, Json = jsonInput, Language = lang, RoleID = finalRoleId }, 
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure);

                    if (string.IsNullOrEmpty(jsonResult))
                    {
                        transaction?.Rollback();
                        return new DbResponse<T> { Success = -1, Message = "No response from database." };
                    }

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<DbResponse<T>>(jsonResult, options) ?? new DbResponse<T> { Success = -1 };

                    // Commit only if Success is 1 (Success) or 0 (Validation Error)
                    // Rollback if Success is -1 (System Error)
                    if (useTransaction && transaction != null)
                    {
                        if (result.Success >= 0) transaction.Commit();
                        else transaction.Rollback();
                    }

                    return result;
                }
                catch
                {
                    transaction?.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return new DbResponse<T> { Success = -1, Message = ex.Message };
            }
        }
    }
}
