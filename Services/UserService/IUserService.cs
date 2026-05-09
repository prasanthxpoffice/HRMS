using HRMS.Models;

namespace HRMS.Services;

public interface IUserService
{
    User? CurrentUser { get; }
    Role? CurrentRole { get; }
    IReadOnlyList<Role> Roles { get; }
    IReadOnlyList<Menu> Menus { get; }
    bool HasAuthenticationError { get; }

    event Action<bool>? OnRoleChanged;

    Task SetCurrentRole(int roleId);
    Task LoadUserAsync();
}
