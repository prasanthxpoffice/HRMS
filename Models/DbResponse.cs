namespace HRMS.Models;

public class DbResponse<T>
{
    public int Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }
}
