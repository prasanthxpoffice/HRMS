using HRMS.Models;

namespace HRMS.Services;

public class UserService : IUserService
{
    private User? _currentUser;
    private Role? _currentRole;
    private List<Role> _roles = new();
    private List<Menu> _menus = new();
    private List<Menu> _allMenus = new();
    private Dictionary<int, List<string>> _roleMenus = new();

    public User? CurrentUser => _currentUser;
    public Role? CurrentRole => _currentRole;
    public IReadOnlyList<Role> Roles => _roles;
    public IReadOnlyList<Menu> Menus => _menus;

    public event Action? OnRoleChanged;

    public async Task LoadUserAsync()
    {
        // TODO: Get Windows identity and authenticate via DB
        // var windowsUser = HttpContext.User.Identity?.Name;
        await Task.CompletedTask;

        LoadHardcodedData();
    }

    public void SetCurrentRole(int roleId)
    {
        var role = _roles.FirstOrDefault(r => r.RoleId == roleId);
        if (role == null || role.RoleId == _currentRole?.RoleId) return;

        _currentRole = role;
        RefreshMenus();
        OnRoleChanged?.Invoke();
    }

    private void LoadHardcodedData()
    {
        _currentUser = new User { UserId = 1, EmployeeId = 1001, UserName = "DOMAIN\\admin" };

        _roles = new List<Role>
        {
            new() { RoleId = 1, RoleName = "Administrator" },
            new() { RoleId = 2, RoleName = "Manager" },
            new() { RoleId = 3, RoleName = "Viewer" }
        };

        _roleMenus = new Dictionary<int, List<string>>
        {
            [1] = new() { "DASHBOARD", "MASTER", "GENDERS" },
            [2] = new() { "DASHBOARD", "MASTER", "GENDERS" },
            [3] = new() { "DASHBOARD" }
        };

        _allMenus = new List<Menu>
        {
            new() { MenuCode = "DASHBOARD", ParentMenuCode = null, MenuNameEn = "Dashboard", MenuNameAr = "لوحة القيادة", Href = "", Icon = "bi-speedometer2", SortOrder = 1 },
            new() { MenuCode = "MASTER", ParentMenuCode = null, MenuNameEn = "Master Data", MenuNameAr = "بيانات التعريف", Href = "#", Icon = "bi-folder", SortOrder = 10 },
            new() { MenuCode = "GENDERS", ParentMenuCode = "MASTER", MenuNameEn = "Genders", MenuNameAr = "الجنس", Href = "genders", Icon = "bi-people", SortOrder = 11 }
        };

        _currentRole = _roles.First();
        RefreshMenus();
    }

    private void RefreshMenus()
    {
        if (_currentRole == null) { _menus = new(); return; }
        if (!_roleMenus.TryGetValue(_currentRole.RoleId, out var allowedCodes)) { _menus = new(); return; }

        var allowed = new HashSet<string>(allowedCodes);
        _menus = _allMenus.Where(m => allowed.Contains(m.MenuCode)).OrderBy(m => m.SortOrder).ToList();
    }
}
