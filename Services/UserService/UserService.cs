using HRMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HRMS.Services;
using DB.Constants;

public class UserService : IUserService
{
    private readonly IDataService _dataService;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IConfiguration _configuration;
    private readonly IUserContext _userContext;
    private readonly LanguageService _languageService;
    
    private User? _currentUser;
    private Role? _currentRole;
    private List<Role> _roles = new();
    private List<Menu> _menus = new();
    private bool _isInitialized;

    public UserService(IDataService dataService, IHttpContextAccessor httpContext, IConfiguration configuration, IUserContext userContext, LanguageService languageService)
    {
        _dataService = dataService;
        _httpContext = httpContext;
        _configuration = configuration;
        _userContext = userContext;
        _languageService = languageService;
    }

    public User? CurrentUser => _currentUser;
    public Role? CurrentRole => _currentRole;
    public IReadOnlyList<Role> Roles => _roles;
    public IReadOnlyList<Menu> Menus => _menus;

    public event Action? OnRoleChanged;

    public async Task LoadUserAsync()
    {
        if (_isInitialized) return;
        _isInitialized = true;

        try
        {
            // 1. Get Windows Identity & App ID (AppId is only needed for Bootstrap Authenticaiton)
            var windowsId = _httpContext.HttpContext?.User.Identity?.Name ?? System.Environment.UserName;
            var appId = _configuration.GetValue<int>("AppId");

            // 2. Fetch User from CentralLogin
            var users = await _dataService.GetListAsync<User>(
                connectionName: ConnectionNames.CentralLogin,
                spName: StoredProcedure.CentralLogin.spGetUserRoles,
                json: new { WindowsId = windowsId, AppId = appId }
            );

            _currentUser = users.FirstOrDefault();
            if (_currentUser != null)
            {
                _roles = _currentUser.AssignedRoles ?? new();
                _currentRole = _roles.FirstOrDefault() ?? new Role { RoleId = 0, RoleName = "Default" };

                // Update the shared UserContext for all future database calls
                _userContext.EmployeeId = _currentUser.EmployeeId;
                _userContext.RoleId = _currentRole.RoleId;

                // 3. Load Menus for the default role
                await LoadMenusAsync();

                OnRoleChanged?.Invoke();
            }
        }
        catch
        {
            _isInitialized = false; 
        }
    }

    public async Task LoadMenusAsync()
    {
        // Fetch menus using automated context (EmployeeId, RoleId, Language)
        // Note: AppId is no longer passed as per USER request
        _menus = await _dataService.GetListAsync<Menu>(
            connectionName: ConnectionNames.CentralLogin,
            spName: StoredProcedure.CentralLogin.spGetUserMenus,
            json: new { } 
        );
    }

    public async void SetCurrentRole(int roleId)
    {
        var role = _roles.FirstOrDefault(r => r.RoleId == roleId);
        if (role == null || role.RoleId == _currentRole?.RoleId) return;

        _currentRole = role;
        
        // Synchronize the shared UserContext
        _userContext.RoleId = _currentRole.RoleId;

        // Refresh Menus for the new role
        await LoadMenusAsync();

        OnRoleChanged?.Invoke();
    }
}
