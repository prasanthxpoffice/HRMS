using Microsoft.AspNetCore.Components;

namespace HRMS.Components.Shared.UI.Grid.Models
{
    public enum UiGridFilterType
    {
        Text,
        Dropdown,
        Date
    }

    public enum AggregateType
    {
        None,
        Count,
        Sum,
        Average,
        Min,
        Max,
        DistinctCount
    }

    public class UiGridColumn<TItem>
    {
        public string Title { get; set; } = string.Empty;
        public string? FieldName { get; set; }
        public RenderFragment<TItem>? Template { get; set; }
        public UiGridFilterType FilterType { get; set; } = UiGridFilterType.Text;
        public bool Sortable { get; set; } = true;
        public string? ColumnGroup { get; set; }
        public AggregateType Aggregate { get; set; } = AggregateType.None;
    }
}
