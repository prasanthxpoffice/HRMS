using HRMS.Models;

namespace HRMS.Services
{
    public class DataService : IDataService
    {
        private readonly IDatabaseService _db;
        private readonly IUserService _userService;
        private readonly INotificationService _notification;

        public DataService(IDatabaseService db, IUserService userService, INotificationService notification)
        {
            _db = db;
            _userService = userService;
            _notification = notification;
        }

        private int CurrentEmployeeId => _userService.CurrentUser?.EmployeeId ?? 0;

        public async Task<List<T>> GetListAsync<T>(string spName, object json)
        {
            var response = await _db.ExecuteQueryAsync<List<T>>(spName, json, CurrentEmployeeId);
            return response?.Data ?? new();
        }

        public async Task<DbResponse<object>> PostDataAsync(string spName, object json)
        {
            var res = await _db.ExecuteQueryAsync<object>(spName, json, CurrentEmployeeId);
            
            if (res != null)
            {
                if (!string.IsNullOrEmpty(res.Message))
                {
                    _notification.Notify(res.Message, res.Success ? NotificationType.Success : NotificationType.Error);
                }
            }

            return res;
        }
    }
}
