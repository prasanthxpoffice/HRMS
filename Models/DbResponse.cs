namespace HRMS.Models;

public enum DbStatus
{
    SystemError = -1,
    BusinessError = 0,
    Success = 1
}

public class DbResponse<T>
{
    public DbStatus Status { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }
}
