using HRMS.Models;

namespace HRMS.Services
{
    public class DataService : IDataService
    {
        private readonly IDatabaseService _db;
        private readonly INotificationService _notification;
        private readonly IResourceService _res;

        public DataService(IDatabaseService db, INotificationService notification, IResourceService res)
        {
            _db = db;
            _notification = notification;
            _res = res;
        }

        public async Task<List<T>> GetListAsync<T>(string connectionName, string spName, object json, bool useTransaction = false)
        {
            var response = await _db.ExecuteQueryAsync<List<T>>(connectionName, spName, json, useTransaction: useTransaction);
            
            if (response != null)
            {
                if (response.Success == -1)
                {
                    _notification.NotifyError(_res.DatabaseError, response.Message);
                }
                else if (response.Success == 0)
                {
                    _notification.Notify(response.Message, NotificationType.Error);
                }
            }

            return response?.Data ?? new();
        }

        public async Task<DbResponse<object>> PostDataAsync(string connectionName, string spName, object json, bool showNotification = true, bool useTransaction = true)
        {
            var res = await _db.ExecuteQueryAsync<object>(connectionName, spName, json, useTransaction: useTransaction);
            
            if (res != null)
            {
                if (res.Success == 1)
                {
                    if (showNotification && !string.IsNullOrEmpty(res.Message))
                        _notification.Notify(res.Message, NotificationType.Success);
                }
                else if (res.Success == 0)
                {
                    _notification.Notify(res.Message, NotificationType.Error);
                }
                else if (res.Success == -1)
                {
                    _notification.NotifyError(_res.DatabaseError, res.Message);
                }
            }

            return res ?? new DbResponse<object> { Success = -1, Message = "Unknown error" };
        }
    }
}
