using HRMS.Models.Export;
using MiniExcelLibs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;
using System.Reflection;

namespace HRMS.Services.ExportService;

public class ExportService : IExportService
{
    static ExportService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> ExportAsync<T>(IEnumerable<T> data, Dictionary<string, string> columns, ExportFormat format, string title)
    {
        var dataTable = ToDataTable(data, columns);

        return format switch
        {
            ExportFormat.Xlsx => GetXlsxBytes(dataTable),
            ExportFormat.Csv => GetCsvBytes(dataTable),
            ExportFormat.Pdf => GetPdfBytes(dataTable, title),
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }

    private DataTable ToDataTable<T>(IEnumerable<T> data, Dictionary<string, string> columns)
    {
        var table = new DataTable();
        if (data == null || !data.Any()) return table;

        // Add columns with localized headers
        foreach (var col in columns)
        {
            table.Columns.Add(col.Value);
        }

        // Get properties from the first item if T is object
        var firstItem = data.First();
        var itemType = firstItem?.GetType() ?? typeof(T);
        var props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Add rows
        foreach (var item in data)
        {
            if (item == null) continue;
            var row = table.NewRow();
            
            foreach (var col in columns)
            {
                // 1. Try Reflection (POCO)
                var prop = props.FirstOrDefault(p => p.Name == col.Key);
                if (prop != null)
                {
                    row[col.Value] = prop.GetValue(item) ?? DBNull.Value;
                }
                // 2. Try Dictionary/Expando (Dynamic/SP)
                else if (item is IDictionary<string, object> dict && dict.ContainsKey(col.Key))
                {
                    row[col.Value] = dict[col.Key] ?? DBNull.Value;
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

    private byte[] GetXlsxBytes(DataTable table)
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

    private byte[] GetPdfBytes(DataTable table, string title)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                page.Header().Text(title).SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                page.Content().PaddingVertical(10).Table(tableContent =>
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
