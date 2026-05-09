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

    public Task<WorkflowResponse> StartProcessAsync<T>(IEnumerable<T> items)
    {
        // TODO: replace with internal function call to get real TransactionId
        var transactionId = Guid.NewGuid().ToString()[..8];

        return Task.FromResult(new WorkflowResponse
        {
            Success = true,
            TransactionId = transactionId
        });
    }
}
