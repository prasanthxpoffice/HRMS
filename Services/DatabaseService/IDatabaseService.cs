namespace HRMS.Services
{
    public interface IDatabaseService
    {
        Task<HRMS.Models.DbResponse<T>> ExecuteQueryAsync<T>(string procedureName, object? jsonParams = null, int? employeeId = null, string? language = null, int? roleId = null);
    }
}
