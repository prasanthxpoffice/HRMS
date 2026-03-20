using HRMS.Models;

namespace HRMS.Services
{
    public interface IDatabaseService
    {
        Task<ApiResponse<T>> ExecuteQueryAsync<T>(string procedureName, object? jsonParams = null, int employeeId = 1, string? language = null);
    }
}
