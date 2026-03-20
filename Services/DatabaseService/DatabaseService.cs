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

        public DatabaseService(IConfiguration configuration, LanguageService languageService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _languageService = languageService;
        }

        public async Task<ApiResponse<T>> ExecuteQueryAsync<T>(string procedureName, object? jsonParams = null, int employeeId = 1, string? language = null)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                return new ApiResponse<T> { Success = false, Message = "Connection string not configured." };
            }

            try
            {
                using IDbConnection db = new SqlConnection(_connectionString);
                
                var lang = language ?? _languageService.CurrentLanguage;
                var jsonInput = jsonParams != null ? JsonSerializer.Serialize(jsonParams) : "{}";

                var jsonResult = await db.QueryFirstOrDefaultAsync<string>(procedureName, 
                    new { EmployeeId = employeeId, Json = jsonInput, Language = lang }, 
                    commandType: CommandType.StoredProcedure);

                if (string.IsNullOrEmpty(jsonResult))
                {
                    return new ApiResponse<T> { Success = false, Message = "No response from database." };
                }

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<ApiResponse<T>>(jsonResult, options) ?? new ApiResponse<T> { Success = false };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T> { Success = false, Message = ex.Message };
            }
        }
    }
}
