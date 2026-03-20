using Microsoft.AspNetCore.Components;
using HRMS.Services;
using HRMS.Models;

namespace HRMS.Components.Shared
{
    public abstract class BaseMasterPage<T> : ComponentBase, IDisposable where T : class
    {
        [Inject] protected ICrudService<T> Service { get; set; } = default!;
        [Inject] protected LanguageService Lang { get; set; } = default!;

        protected List<T> Items = new();

        protected override async Task OnInitializedAsync()
        {
            Lang.OnLanguageChanged += StateHasChanged;
            await LoadData();
        }

        protected virtual async Task LoadData() => Items = await Service.GetAllAsync();

        public void Dispose() => Lang.OnLanguageChanged -= StateHasChanged;
    }
}
