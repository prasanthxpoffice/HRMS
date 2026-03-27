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

        public async Task<DbResponse<T>> ExecuteQueryAsync<T>(string procedureName, object? jsonParams = null, int? employeeId = null, string? language = null)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                return new DbResponse<T> { Success = false, Message = "Connection string not configured." };
            }

            try
            {
                using IDbConnection db = new SqlConnection(_connectionString);
                
                var lang = language ?? _languageService.CurrentLanguage;
                var finalEmployeeId = employeeId ?? _userService.CurrentUser?.EmployeeId ?? 1;
                var jsonInput = jsonParams != null ? JsonSerializer.Serialize(jsonParams) : "{}";

                var jsonResult = await db.QueryFirstOrDefaultAsync<string>(procedureName, 
                    new { EmployeeId = finalEmployeeId, Json = jsonInput, Language = lang }, 
                    commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(jsonResult))
                {
                    return new DbResponse<T> { Success = false, Message = "No response from database." };
                }

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<DbResponse<T>>(jsonResult, options) ?? new DbResponse<T> { Success = false };
            }
            catch (Exception ex)
            {
                return new DbResponse<T> { Success = false, Message = ex.Message };
            }
        }
    }
}
