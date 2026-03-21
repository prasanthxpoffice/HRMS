using Microsoft.AspNetCore.Components;

namespace HRMS.Data
{
    public class UiGridColumn<TItem>
    {
        public string Title { get; set; } = string.Empty;
        public string? FieldName { get; set; }
        public RenderFragment<TItem>? Template { get; set; }
    }
}
