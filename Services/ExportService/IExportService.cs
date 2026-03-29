using HRMS.Models.Export;

namespace HRMS.Services.ExportService;

public interface IExportService
{
    Task<byte[]> ExportAsync<T>(IEnumerable<T> data, Dictionary<string, string> columns, ExportFormat format, string title);
}
