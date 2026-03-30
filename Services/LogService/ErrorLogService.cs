namespace HRMS.Services.LogService
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IWebHostEnvironment _env;

        public ErrorLogService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task LogErrorAsync(Exception ex, int employeeId, int roleId, string connectionName, string procedureName, string jsonPayload)
        {
            try
            {
                // Create ErrorLogs directory at the root of the project
                string logDirectory = Path.Combine(_env.ContentRootPath, "ErrorLogs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Append to a daily log file
                string logFile = Path.Combine(logDirectory, $"ErrorLog_{DateTime.Now:yyyyMMdd}.txt");

                // Format the error message block
                string logEntry = $@"
=========================================================
Timestamp     : {DateTime.Now:yyyy-MM-dd HH:mm:ss}
User IDs      : EmployeeId: {employeeId} | RoleId: {roleId}
Target DB     : Connection: {connectionName}
Target Proc   : {procedureName}
JSON Payload  : {jsonPayload}
Error Message : {ex.Message}
Stack Trace   : 
{ex.StackTrace}
=========================================================
";
                // Asynchronously write to file
                await File.AppendAllTextAsync(logFile, logEntry);
            }
            catch
            {
                // Failsafe: If the logger completely fails (e.g., file permissions), we swallow the error 
                // so we don't crash the UI popup gracefully mapping the original database error.
            }
        }
    }
}
