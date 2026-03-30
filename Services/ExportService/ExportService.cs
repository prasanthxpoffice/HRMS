using HRMS.Models.Export;
using MiniExcelLibs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;
using System.Reflection;
using System.Text.Json;

namespace HRMS.Services.ExportService;

public class ExportService : IExportService
{
    private readonly IResourceService _res;

    public ExportService(IResourceService res)
    {
        _res = res;
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public Task<byte[]> ExportAsync<T>(IEnumerable<T> data, Dictionary<string, string>? columns, ExportFormat format, string title, bool isRtl = false)
    {
        return Task.Run(() => 
        {
            var dataTable = ToDataTable(data, columns);

            return format switch
            {
                ExportFormat.Xlsx => GetXlsxBytes(dataTable, isRtl),
                ExportFormat.Csv => GetCsvBytes(dataTable),
                ExportFormat.Pdf => GetPdfBytes(dataTable, title, isRtl),
                _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
            };
        });
    }

    private DataTable ToDataTable<T>(IEnumerable<T> data, Dictionary<string, string>? columns)
    {
        var table = new DataTable();
        if (data == null || !data.Any()) return table;

        var firstItem = data.First();
        if (firstItem == null) return table;

        var finalColumns = columns ?? new Dictionary<string, string>();

        // 1. If no columns provided, detect them from the data automatically
        if (columns == null || !columns.Any())
        {
            if (firstItem is JsonElement element && element.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in element.EnumerateObject())
                {
                    var translatedTitle = _res.GetString(prop.Name) ?? prop.Name;
                    finalColumns.Add(prop.Name, translatedTitle);
                }
            }
            else
            {
                var itemType = firstItem.GetType();
                foreach (var prop in itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var translatedTitle = _res.GetString(prop.Name) ?? prop.Name;
                    finalColumns.Add(prop.Name, translatedTitle);
                }
            }
        }

        // Add columns to table
        foreach (var col in finalColumns)
        {
            table.Columns.Add(col.Value);
        }

        // Add rows
        var itemTypeRef = firstItem.GetType();
        var propsRef = itemTypeRef.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var item in data)
        {
            if (item == null) continue;
            var row = table.NewRow();
            IDictionary<string, object>? dict = item as IDictionary<string, object>;

            foreach (var col in finalColumns)
            {
                // 1. Try Reflection (POCO)
                var prop = propsRef.FirstOrDefault(p => string.Equals(p.Name, col.Key, StringComparison.OrdinalIgnoreCase));
                if (prop != null)
                {
                    row[col.Value] = prop.GetValue(item) ?? DBNull.Value;
                }
                // 2. Try Dictionary
                else if (dict != null)
                {
                    var dictEntry = dict.FirstOrDefault(e => string.Equals(e.Key, col.Key, StringComparison.OrdinalIgnoreCase));
                    if (!string.IsNullOrEmpty(dictEntry.Key))
                        row[col.Value] = dictEntry.Value ?? DBNull.Value;
                    else
                        row[col.Value] = DBNull.Value;
                }
                // 3. Try JsonElement
                else if (item is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Object)
                {
                    bool found = false;
                    foreach (var jsonProp in jsonElement.EnumerateObject())
                    {
                        if (string.Equals(jsonProp.Name, col.Key, StringComparison.OrdinalIgnoreCase))
                        {
                            row[col.Value] = GetJsonValue(jsonProp.Value);
                            found = true;
                            break;
                        }
                    }
                    if (!found) row[col.Value] = DBNull.Value;
                }
                else
                {
                    row[col.Value] = DBNull.Value;
                }
            }
            table.Rows.Add(row);
        }

        return table;
    }

    private object GetJsonValue(JsonElement element)
    {
        return element.ValueKind switch
        {
            JsonValueKind.String => element.GetString() ?? "",
            JsonValueKind.Number => element.TryGetDecimal(out var d) ? d : element.GetDouble(),
            JsonValueKind.True => true,
            JsonValueKind.False => false,
            JsonValueKind.Null => DBNull.Value,
            _ => element.GetRawText()
        };
    }

    private byte[] GetXlsxBytes(DataTable table, bool isRtl = false)
    {
        using var ms = new MemoryStream();
        ms.SaveAs(table);
        return ms.ToArray();
    }

    private byte[] GetCsvBytes(DataTable table)
    {
        using var ms = new MemoryStream();
        ms.SaveAs(table, excelType: ExcelType.CSV);
        return ms.ToArray();
    }

    private byte[] GetPdfBytes(DataTable table, string title, bool isRtl = false)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                var header = page.Header().Text(title).SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);
                if (isRtl) header.AlignRight();
                else header.AlignLeft();

                var content = page.Content().PaddingVertical(10);
                if (isRtl)
                {
                    content = content.ContentFromRightToLeft();
                }

                content.Table(tableContent =>
                {
                    // Define columns
                    tableContent.ColumnsDefinition(columns =>
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            columns.RelativeColumn();
                        }
                    });

                    // Header
                    tableContent.Header(header =>
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            header.Cell().Background(Colors.Grey.Lighten3).Border(1).Padding(5).Text(column.ColumnName).SemiBold();
                        }
                    });

                    // Rows
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (var cellValue in row.ItemArray)
                        {
                            tableContent.Cell().Border(1).Padding(5).Text(cellValue?.ToString() ?? "");
                        }
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Page ");
                    x.CurrentPageNumber();
                });
            });
        });

        return document.GeneratePdf();
    }
}
