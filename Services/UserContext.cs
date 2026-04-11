namespace HRMS.Services;

public class UserContext : IUserContext
{
    public int EmployeeId { get; set; } = 0; // Default to 0 for initial bootstrap
    public int RoleId { get; set; } = 0;
}
