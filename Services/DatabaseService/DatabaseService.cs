using Dapper;
using HRMS.Models;
using HRMS.Resources;
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
                return new DbResponse<T> { Status = DbStatus.SystemError, Message = "Connection string not configured." };
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

                await using SqlTransaction? transaction = useTransaction ? (SqlTransaction)await db.BeginTransactionAsync() : null;

                try
                {
                    var jsonResult = await db.QueryFirstOrDefaultAsync<string>(procedureName,
                        new { EmployeeId = finalEmployeeId, Json = jsonInput, Language = lang, RoleID = finalRoleId },
                        transaction: transaction,
                        commandType: CommandType.StoredProcedure);

                    if (string.IsNullOrEmpty(jsonResult))
                    {
                        if (transaction != null) await transaction.RollbackAsync();
                        return new DbResponse<T> { Status = DbStatus.SystemError, Message = "No response from database." };
                    }

                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<DbResponse<T>>(jsonResult, options) ?? new DbResponse<T> { Status = DbStatus.SystemError };

                    if (useTransaction && transaction != null)
                    {
                        if (result.Status != DbStatus.SystemError) await transaction.CommitAsync();
                        else await transaction.RollbackAsync();
                    }

                    return result;
                }
                catch
                {
                    if (transaction != null) await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                _ = _logService.LogErrorAsync(ex, finalEmployeeId, finalRoleId, connectionName, procedureName, jsonInput);
                return new DbResponse<T> { Status = DbStatus.SystemError, Message = AppResources.DatabaseErrorMessage };
            }
        }
    }
}
