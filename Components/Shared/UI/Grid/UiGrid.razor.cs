using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HRMS.Services;
using System.Reflection;
using System.Globalization;
using HRMS.Components.Shared.UI;
using HRMS.Components.Shared.UI.Grid.Models;

using HRMS.Models.Export;
using HRMS.Services.ExportService;
using Microsoft.JSInterop;
using HRMS.Services.WorkflowService;
using HRMS.Resources;

namespace HRMS.Components.Shared.UI.Grid;

public partial class UiGrid<TItem> : UiBase
{
    [Inject] protected IDataService DataService { get; set; } = default!;
    [Inject] public IWorkflowService WorkflowService { get; set; } = default!;
    [Inject] public IExportService ExportService { get; set; } = default!;
    [Inject] public IJSRuntime JS { get; set; } = default!;

    #region Parameters - Configuration
    [Parameter] public string Title { get; set; } = "";
    [Parameter] public string Icon { get; set; } = "bi-list-stars";
    [Parameter] public List<TItem> Items { get; set; } = new();
    [Parameter] public List<UiGridColumn<TItem>> Columns { get; set; } = new();
    [Parameter] public RenderFragment? HeaderContent { get; set; }
    [Parameter] public int PageSize { get; set; } = 10;
    [Parameter] public bool ShowHeader { get; set; } = true;
    [Parameter] public bool Loading { get; set; } = false;
    #endregion

    #region Parameters - Actions
    [Parameter] public bool ShowAddButton { get; set; } = false;
    [Parameter] public string? AddButtonText { get; set; }
    [Parameter] public EventCallback OnAddClick { get; set; }
    #endregion

    #region Parameters - Selection & Deletion
    [Parameter] public bool ShowSelection { get; set; } = false;
    [Parameter] public bool ShowDeleteButton { get; set; } = false;
    [Parameter] public HashSet<object> SelectedIds { get; set; } = new();
    [Parameter] public EventCallback<HashSet<object>> SelectedIdsChanged { get; set; }
    [Parameter] public EventCallback<HashSet<object>> OnDeleteSelected { get; set; }
    
    // Legacy support (optional, can be phased out)
    [Parameter] public HashSet<TItem> SelectedItems { get; set; } = new();
    [Parameter] public EventCallback<HashSet<TItem>> SelectedItemsChanged { get; set; }
    
    [Parameter] public string? DeleteSpName { get; set; }
    [Parameter] public string? DeleteConnectionName { get; set; }
    [Parameter] public string? IdFieldName { get; set; }
    [Parameter] public EventCallback OnDataChanged { get; set; }
    
    [Parameter] public bool ShowWorkflow { get; set; } = false;
    [Parameter] public string? WorkflowTitle { get; set; }
    [Parameter] public bool ShowExport { get; set; } = true;
    [Parameter] public Func<Task<IEnumerable<object>>>? OnGetExportData { get; set; }
    [Parameter] public string? ExportName { get; set; }
    [Parameter] public EventCallback<IEnumerable<TItem>> OnWorkflowClick { get; set; }
#endregion

    #region Internal State
    private Dictionary<string, string> _columnFilters = new();
    private int CurrentPage { get; set; } = 1;
    private string? _sortField;
    private bool _sortDescending;
    private bool _showConfirmModal = false;
    private TItem? _itemPendingDelete;
    private bool _exportGridDataOnly = true;
    private bool _isAllPagesSelected = false;
    #endregion

    #region Data Processing - Filtering & Sorting
    private IEnumerable<TItem> FilteredItems
    {
        get
        {
            var data = (Items ?? new List<TItem>()).AsEnumerable();

            // Column filters
            foreach (var filter in _columnFilters)
            {
                if (!string.IsNullOrWhiteSpace(filter.Value))
                {
                    var col = Columns.FirstOrDefault(c => c.FieldName == filter.Key);
                    if (col == null) continue;

                    data = data.Where(i => {
                        var rawVal = GetPropertyValue(i, filter.Key);
                        var valStr = rawVal?.ToString() ?? "";

                        if (col.FilterType == UiGridFilterType.Dropdown)
                        {
                            return valStr.Equals(filter.Value, StringComparison.OrdinalIgnoreCase);
                        }
                        
                        if (col.FilterType == UiGridFilterType.Date)
                        {
                            if (DateTime.TryParse(filter.Value, out var filterDate))
                            {
                                if (rawVal is DateTime dt) return dt.Date == filterDate.Date;
                                
                                if (DateTime.TryParseExact(valStr, "dd/MM/yyyy", null, DateTimeStyles.None, out var itemDate))
                                {
                                    return itemDate.Date == filterDate.Date;
                                }
                                if (DateTime.TryParse(valStr, out var fallbackDate))
                                {
                                    return fallbackDate.Date == filterDate.Date;
                                }
                            }
                            return false;
                        }

                        return valStr.Contains(filter.Value, StringComparison.OrdinalIgnoreCase);
                    });
                }
            }

            // Sorting
            if (!string.IsNullOrEmpty(_sortField))
            {
                var col = Columns.FirstOrDefault(c => c.FieldName == _sortField);
                if (col != null)
                {
                    data = _sortDescending 
                        ? data.OrderByDescending(i => GetPropertyValue(i, _sortField))
                        : data.OrderBy(i => GetPropertyValue(i, _sortField));
                }
            }

            return data;
        }
    }

    private int TotalItems => FilteredItems.Count();
    private int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

    private IEnumerable<TItem> PagedItems => FilteredItems
        .Skip((CurrentPage - 1) * PageSize)
        .Take(PageSize);
    #endregion

    #region Event Handlers - Navigation & Sorting
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        if (!string.IsNullOrEmpty(DeleteSpName) && string.IsNullOrEmpty(DeleteConnectionName))
        {
            throw new InvalidOperationException("DeleteConnectionName is strongly required when DeleteSpName is specified.");
        }
    }

    private void GoToPage(int page) => CurrentPage = Math.Clamp(page, 1, TotalPages == 0 ? 1 : TotalPages);

    private void HandleSort(UiGridColumn<TItem> col)
    {
        if (!col.Sortable || string.IsNullOrEmpty(col.FieldName)) return;

        if (_sortField == col.FieldName)
        {
            if (!_sortDescending) _sortDescending = true;
            else _sortField = null; // Reset sort
        }
        else
        {
            _sortField = col.FieldName;
            _sortDescending = false;
        }
        StateHasChanged();
    }

    private void OnPageSizeChanged(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out var size))
        {
            PageSize = size;
            CurrentPage = 1;
        }
    }

    private List<int> GetPageNumbers()
    {
        var numbers = new List<int>();
        if (TotalPages <= 7)
        {
            for (int i = 1; i <= TotalPages; i++) numbers.Add(i);
        }
        else
        {
            numbers.Add(1);
            if (CurrentPage > 4) numbers.Add(-1);
            
            var start = Math.Max(2, CurrentPage - 2);
            var end = Math.Min(TotalPages - 1, CurrentPage + 2);
            for (int i = start; i <= end; i++) numbers.Add(i);

            if (CurrentPage < TotalPages - 3) numbers.Add(-1);
            numbers.Add(TotalPages);
        }
        return numbers;
    }
    #endregion

    #region Event Handlers - Selection
    private bool IsAllSelected => PagedItems.Any() && PagedItems.All(i => IsSelected(i));

    private bool IsSelected(TItem item)
    {
        if (_isAllPagesSelected) return true;
        var id = GetPropertyValue(item, IdFieldName);
        return id != null && SelectedIds.Contains(id);
    }

    private async Task ToggleSelectAll(ChangeEventArgs e)
    {
        bool isChecked = (bool)(e.Value ?? false);
        _isAllPagesSelected = false; // Always reset global selection when toggling page

        foreach (var item in PagedItems)
        {
            var id = GetPropertyValue(item, IdFieldName);
            if (id == null) continue;

            if (isChecked) SelectedIds.Add(id);
            else SelectedIds.Remove(id);
        }
        await NotifySelectionChanged();
    }

    private async Task ToggleSelection(TItem item)
    {
        var id = GetPropertyValue(item, IdFieldName);
        if (id == null) return;

        if (_isAllPagesSelected)
        {
            _isAllPagesSelected = false;
            // Transition from Global to Manual: Select everything on the current page EXCEPT the one toggled
            SelectedIds.Clear();
            foreach (var pItem in PagedItems)
            {
                var pId = GetPropertyValue(pItem, IdFieldName);
                if (pId != null && !pId.Equals(id)) SelectedIds.Add(pId);
            }
        }
        else
        {
            if (SelectedIds.Contains(id))
            {
                SelectedIds.Remove(id);
            }
            else
            {
                SelectedIds.Add(id);
            }
        }
        await NotifySelectionChanged();
    }

    private async Task SelectAllFiltered(bool value)
    {
        _isAllPagesSelected = value;
        SelectedIds.Clear();
        await NotifySelectionChanged();
    }

    private async Task NotifySelectionChanged()
    {
        await SelectedIdsChanged.InvokeAsync(SelectedIds);
        
        // Update legacy SelectedItems for compatibility if needed
        SelectedItems.Clear();
        foreach (var item in Items ?? new())
        {
            var id = GetPropertyValue(item, IdFieldName);
            if (id != null && SelectedIds.Contains(id)) SelectedItems.Add(item);
        }
        await SelectedItemsChanged.InvokeAsync(SelectedItems);
    }
    #endregion

    #region Event Handlers - Filtering
    private string GetFilterValue(string field) => _columnFilters.TryGetValue(field, out var val) ? val : "";

    private void SetFilterValue(string field, string? value)
    {
        _columnFilters[field] = value ?? "";
        CurrentPage = 1;
    }

    private List<string> GetDistinctValues(string fieldName)
    {
        if (string.IsNullOrEmpty(fieldName) || Items == null) return new();
        return Items.Select(i => GetPropertyValue(i, fieldName)?.ToString() ?? "")
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Distinct()
                    .OrderBy(s => s)
                    .ToList();
    }
    #endregion

    #region Business Logic - Deletion
    private void HandleDeleteSelected()
    {
        if (!_isAllPagesSelected && (SelectedIds == null || !SelectedIds.Any())) return;
        
        _itemPendingDelete = default;
        _showConfirmModal = true;
    }

    private async Task ConfirmDelete()
    {
        _showConfirmModal = false;

        if (_itemPendingDelete != null)
        {
            await ProcessSingleDelete(_itemPendingDelete);
        }
        else
        {
            await ProcessBulkDelete();
        }
    }

    private async Task ProcessSingleDelete(TItem item)
    {
        if (string.IsNullOrEmpty(DeleteSpName) || string.IsNullOrEmpty(IdFieldName)) return;

        var idVal = GetPropertyValue(item, IdFieldName);
        if (idVal == null) return;

        var param = new Dictionary<string, object> { { IdFieldName, idVal } };
        var res = await DataService.PostDataAsync(DeleteConnectionName!, DeleteSpName, param);
        
        if (res.Success == 1)
        {
            SelectedIds.Remove(idVal);
            await NotifySelectionChanged();
            await OnDataChanged.InvokeAsync();
        }
    }

    private async Task ProcessBulkDelete()
    {
        if (string.IsNullOrEmpty(DeleteSpName) || string.IsNullOrEmpty(IdFieldName))
        {
            var itemsToDelete = _isAllPagesSelected ? FilteredItems : Items.Where(IsSelected);
            await OnDeleteSelected.InvokeAsync(SelectedIds); // Warning: Callback might need updating to handle IDs
            return;
        }

        int successCount = 0;
        int errorCount = 0;

        var itemsToProcess = _isAllPagesSelected ? FilteredItems.ToList() : Items.Where(IsSelected).ToList();

        foreach (var item in itemsToProcess)
        {
            var idVal = GetPropertyValue(item, IdFieldName);
            if (idVal == null) continue;

            var param = new Dictionary<string, object> { { IdFieldName, idVal } };
            var res = await DataService.PostDataAsync(DeleteConnectionName!, DeleteSpName, param, showNotification: false);
            
            if (res.Success == 1) successCount++;
            else errorCount++;
        }

        if (successCount > 0) NotificationService.Notify(string.Format(AppResources.BulkDeleteSuccess, successCount), NotificationType.Success);
        if (errorCount > 0) NotificationService.Notify(string.Format(AppResources.BulkDeleteError, errorCount), NotificationType.Error);

        _isAllPagesSelected = false;
        SelectedIds.Clear();
        await NotifySelectionChanged();
        await OnDataChanged.InvokeAsync();
    }

    public void RequestDelete(TItem item)
    {
        _itemPendingDelete = item;
        _showConfirmModal = true;
        StateHasChanged();
    }

    private async Task HandleWorkflowClick()
    {
        var itemsToProcess = _isAllPagesSelected ? FilteredItems : Items.Where(IsSelected);
        if (!itemsToProcess.Any()) return;

        // If a manual callback is provided, use it. Otherwise use the default service logic.
        if (OnWorkflowClick.HasDelegate)
        {
            await OnWorkflowClick.InvokeAsync(itemsToProcess);
        }
        else
        {
            var response = await WorkflowService.StartProcessAsync(itemsToProcess);
            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.Message))
                {
                    NotificationService.Notify(response.Message, NotificationType.Info);
                }

                if (!string.IsNullOrEmpty(response.TransactionId))
                {
                    WorkflowService.OpenViewer(response.TransactionId, WorkflowTitle ?? AppResources.Workflow);
                }
            }
        }
    }

    private async Task HandleExport(ExportFormat format)
    {
        try
        {
            IsLoading = true;
            IEnumerable<object> exportData;
            Dictionary<string, string>? exportColumns = null;

            if (OnGetExportData != null && !_exportGridDataOnly)
            {
                // Mode 1: Manual Callback (Auto-detect all fields from data)
                exportData = await OnGetExportData.Invoke();
                exportColumns = null; // Triggers auto-detection in ExportService
            }
            else
            {
                // Mode 2: Standard Grid Export (Selection or Filters)
                // We only export the columns defined in the grid.
                exportColumns = Columns
                    .Where(c => !string.IsNullOrEmpty(c.FieldName))
                    .ToDictionary(c => c.FieldName!, c => c.Title!);

                if (_isAllPagesSelected)
                {
                    exportData = FilteredItems.Cast<object>();
                }
                else if (SelectedIds.Any())
                {
                    exportData = Items.Where(IsSelected).Cast<object>();
                }
                else
                {
                    exportData = FilteredItems.Cast<object>();
                }
            }

            if (exportData == null || !exportData.Any())
            {
                NotificationService.Notify(AppResources.NoRecordsFound, NotificationType.Warning);
                return;
            }

            var fileName = $"{(string.IsNullOrEmpty(ExportName) ? "Data" : ExportName)}_{DateTime.Now:yyyyMMddHHmmss}";
            var (extension, contentType) = format switch
            {
                ExportFormat.Xlsx => ("xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
                ExportFormat.Csv => ("csv", "text/csv"),
                ExportFormat.Pdf => ("pdf", "application/pdf"),
                _ => ("bin", "application/octet-stream")
            };
 
            var finalTitle = string.IsNullOrEmpty(Title) ? (AppResources.AdminDashboard ?? "HRMS Report") : Title;
            var bytes = await ExportService.ExportAsync(exportData, exportColumns, format, finalTitle, Lang.IsRtl);
            await JS.InvokeVoidAsync("downloadFileFromStream", $"{fileName}.{extension}", bytes, contentType);
            
            string successMsg = AppResources.ExportSuccess ?? (Lang.IsRtl ? "تم تصدير {0} سجلات بنجاح." : "Successfully exported {0} records.");
            NotificationService.Notify(successMsg.Replace("{0}", exportData.Count().ToString()), NotificationType.Success); 
        }
        catch (Exception ex)
        {
            NotificationService.Notify($"{AppResources.SystemError}: {ex.Message}", NotificationType.Error);
        }
        finally
        {
            IsLoading = false;
        }
    }
    #endregion

    #region Helpers
    private object? GetPropertyValue(TItem item, string? fieldName)
    {
        if (string.IsNullOrEmpty(fieldName)) return null;
        return item?.GetType().GetProperty(fieldName)?.GetValue(item);
    }
    #endregion
}
