using Microsoft.AspNetCore.Components;
using HRMS.Services;

namespace HRMS.Components.Shared.UI;

public class UiBase : ComponentBase, IDisposable
{
    [Inject] protected LanguageService Lang { get; set; } = default!;
    [Inject] protected IResourceService Res { get; set; } = default!;

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
        Lang.OnLanguageChanged += HandleLanguageChange;
    }

    private void HandleLanguageChange()
    {
        InvokeAsync(StateHasChanged);
    }

    public virtual void Dispose()
    {
        Lang.OnLanguageChanged -= HandleLanguageChange;
    }
}
