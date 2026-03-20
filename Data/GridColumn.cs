using Microsoft.AspNetCore.Components;

namespace HRMS.Data
{
    public class GridColumn<TItem>
    {
        public string Title { get; set; } = "";
        public string? FieldName { get; set; }
        public RenderFragment<TItem>? Template { get; set; }
    }
}
