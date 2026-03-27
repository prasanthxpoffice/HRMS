namespace HRMS.Services
{
    public interface IDataService
    {
        Task<List<T>> GetListAsync<T>(string spName, object json, int? employeeId = null, string? language = null, int? roleId = null, bool useTransaction = false);
        Task<HRMS.Models.DbResponse<object>> PostDataAsync(string spName, object json, bool showNotification = true, int? employeeId = null, string? language = null, int? roleId = null, bool useTransaction = true);
    }
}
