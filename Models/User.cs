namespace HRMS.Models;

public class User
{
    public int EmployeeId { get; set; }
    public string UserName { get; set; } = "";
    public List<Role> AssignedRoles { get; set; } = new();
}
