using HRMS.Models;
using HRMS.Resources;

namespace HRMS.Services
{
    public class DataService : IDataService
    {
        private readonly IDatabaseService _db;
        private readonly INotificationService _notification;

        public DataService(IDatabaseService db, INotificationService notification)
        {
            _db = db;
            _notification = notification;
        }

        public async Task<List<T>> GetListAsync<T>(string connectionName, string spName, object json, bool useTransaction = false)
        {
            var response = await _db.ExecuteQueryAsync<List<T>>(connectionName, spName, json, useTransaction: useTransaction);
            
            if (response?.Status == DbStatus.SystemError)
                _notification.NotifyError(AppResources.DatabaseError, response.Message);

            return response?.Data ?? new();
        }

        public async Task<DbResponse<object>> PostDataAsync(string connectionName, string spName, object json, bool showNotification = true, bool useTransaction = true)
        {
            var res = await _db.ExecuteQueryAsync<object>(connectionName, spName, json, useTransaction: useTransaction);
            
            if (res != null)
            {
                if (res.Status == DbStatus.Success)
                {
                    if (showNotification && !string.IsNullOrEmpty(res.Message))
                        _notification.Notify(res.Message, NotificationType.Success);
                }
                else if (res.Status == DbStatus.BusinessError)
                {
                    _notification.Notify(res.Message, NotificationType.Error);
                }
                else if (res.Status == DbStatus.SystemError)
                {
                    _notification.NotifyError(AppResources.DatabaseError, res.Message);
                }
            }

            return res ?? new DbResponse<object> { Status = DbStatus.SystemError, Message = "Unknown error" };
        }
    }
}
