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
        private readonly IUserContext _userContext;
        private readonly HRMS.Services.LogService.IErrorLogService _logService;

        public DatabaseService(
            IConfiguration configuration, 
            LanguageService languageService, 
            IUserContext userContext,
            HRMS.Services.LogService.IErrorLogService logService)
        {
            _configuration = configuration;
            _languageService = languageService;
            _userContext = userContext;
            _logService = logService;
        }

        public async Task<DbResponse<T>> ExecuteQueryAsync<T>(string connectionName, string procedureName, object? jsonParams = null, bool useTransaction = false)
        {
            string connectionString = _configuration.GetConnectionString(connectionName) ?? "";
            
            if (string.IsNullOrEmpty(connectionString))
            {
                return new DbResponse<T> { Success = -1, Message = "Connection string not configured." };
            }

            // Automatically resolve context values
            var finalEmployeeId = _userContext.EmployeeId;
            var finalRoleId = _userContext.RoleId;
            var jsonInput = jsonParams != null ? JsonSerializer.Serialize(jsonParams) : "{}";
            var lang = _languageService.CurrentLanguage;

            try
            {
                using var db = new SqlConnection(connectionString);
                await db.OpenAsync();

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
                _ = _logService.LogErrorAsync(ex, finalEmployeeId, finalRoleId, connectionName, procedureName, jsonInput);
                return new DbResponse<T> { Success = -1, Message = ex.Message };
            }
        }
    }
}
