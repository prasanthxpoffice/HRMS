using Microsoft.AspNetCore.Components;
using HRMS.Services;

namespace HRMS.Components.Shared.UI;

public class UiBase : ComponentBase, IDisposable
{
    [Inject] public LanguageService Lang { get; set; } = default!;
    [Inject] public IResourceService Res { get; set; } = default!;
    [Inject] public INotificationService NotificationService { get; set; } = default!;
    [Inject] public IUserService UserService { get; set; } = default!;
    [Inject] public NavigationManager Navigation { get; set; } = default!;

    protected bool IsLoading { get; set; }
    protected bool IsSaving { get; set; }

    protected async Task LoadAsync(Func<Task> action)
    {
        try
        {
            IsLoading = true;
            await action();
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected async Task SaveAsync(Func<Task> action)
    {
        try
        {
            IsSaving = true;
            await action();
        }
        finally
        {
            IsSaving = false;
        }
    }

    protected override void OnInitialized()
    {
        UserService.OnRoleChanged += HandleRoleChanged;
    }

    private void HandleRoleChanged()
    {
        Navigation.NavigateTo("/");
        InvokeAsync(StateHasChanged);
    }

    public virtual void Dispose()
    {
        UserService.OnRoleChanged -= HandleRoleChanged;
    }
}
