namespace HRMS.Services
{
    public interface IDataService
    {
        Task<List<T>> GetListAsync<T>(string spName, object json);
        Task<HRMS.Models.DbResponse<object>> PostDataAsync(string spName, object json);
    }
}
