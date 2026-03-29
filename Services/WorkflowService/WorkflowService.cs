using HRMS.Models.Workflow;
using Microsoft.Extensions.Configuration;

namespace HRMS.Services.WorkflowService;

public class WorkflowService : IWorkflowService
{
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;

    public bool IsModalVisible { get; private set; }
    public string? CurrentTransactionId { get; private set; }
    public string? ModalTitle { get; private set; }
    public event Action? OnStateChanged;

    public WorkflowService(IConfiguration configuration)
    {
        _configuration = configuration;
        _baseUrl = _configuration["Workflow:BaseUrl"] ?? "https://workflow-engine.example.com/viewer";
    }

    private void NotifyStateChanged() => OnStateChanged?.Invoke();

    public void OpenViewer(string transactionId, string? title = null)
    {
        CurrentTransactionId = transactionId;
        ModalTitle = title ?? "Workflow";
        IsModalVisible = true;
        NotifyStateChanged();
    }

    public void CloseViewer()
    {
        IsModalVisible = false;
        NotifyStateChanged();
    }

    public async Task<WorkflowResponse> StartProcessAsync<T>(IEnumerable<T> items)
    {
        // Dummy implementation as requested
        await Task.Delay(500); // Simulate network latency

        if (items.Count() > 5)
        {
            return new WorkflowResponse
            {
                Success = true,
                Message = "Large batch processing initiated. You will be notified via email upon completion.",
                TransactionId = null
            };
        }

        return new WorkflowResponse
        {
            Success = true,
            Message = null,
            TransactionId = Guid.NewGuid().ToString().Substring(0, 8)
        };
    }
}
