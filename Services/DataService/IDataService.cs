using HRMS.Models;

namespace HRMS.Services
{
    public interface IDataService
    {
        Task<List<T>> GetListAsync<T>(string spName, object json);
        Task<ApiResponse<object>> PostDataAsync(string spName, object json);
    }
}
