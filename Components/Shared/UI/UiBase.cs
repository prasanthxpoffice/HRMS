using Microsoft.AspNetCore.Components;
using HRMS.Services;

namespace HRMS.Components.Shared.UI;

public class UiBase : ComponentBase, IDisposable
{
    [Inject] protected LanguageService Lang { get; set; } = default!;
    [Inject] protected IResourceService Res { get; set; } = default!;

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
