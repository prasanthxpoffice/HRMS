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

        public async Task<List<T>> GetListAsync<T>(string spName, object json, int? employeeId = null, string? language = null)
        {
            var response = await _db.ExecuteQueryAsync<List<T>>(spName, json, employeeId, language);
            return response?.Data ?? new();
        }

        public async Task<DbResponse<object>> PostDataAsync(string spName, object json, bool showNotification = true, int? employeeId = null, string? language = null)
        {
            var res = await _db.ExecuteQueryAsync<object>(spName, json, employeeId, language);
            
            if (res != null && showNotification)
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
