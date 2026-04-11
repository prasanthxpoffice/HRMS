namespace HRMS.Services
{
    public interface IDatabaseService
    {
        Task<HRMS.Models.DbResponse<T>> ExecuteQueryAsync<T>(string connectionName, string procedureName, object? jsonParams = null, bool useTransaction = false);
    }
}
