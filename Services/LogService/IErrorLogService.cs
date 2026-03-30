namespace HRMS.Services.LogService
{
    public interface IErrorLogService
    {
        Task LogErrorAsync(
            Exception ex, 
            int employeeId, 
            int roleId, 
            string connectionName, 
            string procedureName, 
            string jsonPayload);
    }
}
