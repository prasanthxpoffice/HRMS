using HRMS.Models;

namespace HRMS.Services
{
    public class DataService : IDataService
    {
        private readonly IDatabaseService _db;
        private readonly IUserService _userService;

        public DataService(IDatabaseService db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        private int CurrentEmployeeId => _userService.CurrentUser?.EmployeeId ?? 0;

        public async Task<List<T>> GetListAsync<T>(string spName, object json)
        {
            var response = await _db.ExecuteQueryAsync<List<T>>(spName, json, CurrentEmployeeId);
            return response?.Data ?? new();
        }

        public async Task<ApiResponse<object>> PostDataAsync(string spName, object json)
        {
            return await _db.ExecuteQueryAsync<object>(spName, json, CurrentEmployeeId);
        }
    }
}
