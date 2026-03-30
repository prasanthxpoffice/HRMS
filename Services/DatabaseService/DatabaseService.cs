using Dapper;
using HRMS.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace HRMS.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;
        private readonly LanguageService _languageService;
        private readonly IUserService _userService;
        private readonly HRMS.Services.LogService.IErrorLogService _logService;

        public DatabaseService(
            IConfiguration configuration, 
            LanguageService languageService, 
            IUserService userService,
            HRMS.Services.LogService.IErrorLogService logService)
        {
            _configuration = configuration;
            _languageService = languageService;
            _userService = userService;
            _logService = logService;
        }

        public async Task<DbResponse<T>> ExecuteQueryAsync<T>(string connectionName, string procedureName, object? jsonParams = null, int? employeeId = null, string? language = null, int? roleId = null, bool useTransaction = false)
        {
            string connectionString = _configuration.GetConnectionString(connectionName) ?? "";
            
            if (string.IsNullOrEmpty(connectionString))
            {
                return new DbResponse<T> { Success = -1, Message = "Connection string not configured." };
            }

            var finalEmployeeId = employeeId ?? _userService.CurrentUser?.EmployeeId ?? 1;
            var finalRoleId = roleId ?? _userService.CurrentRole?.RoleId ?? 0;
            var jsonInput = jsonParams != null ? JsonSerializer.Serialize(jsonParams) : "{}";

            try
            {
                using var db = new SqlConnection(connectionString);
                await db.OpenAsync();

                var lang = language ?? _languageService.CurrentLanguage;

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
                // Silently log the exact DB crash context to the flat file.
                _ = _logService.LogErrorAsync(ex, finalEmployeeId, finalRoleId, connectionName, procedureName, jsonInput);

                return new DbResponse<T> { Success = -1, Message = ex.Message };
            }
        }
    }
}
