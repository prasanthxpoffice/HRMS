using HRMS.Models.Workflow;

namespace HRMS.Services.WorkflowService;

public interface IWorkflowService
{
    bool IsModalVisible { get; }
    string? CurrentTransactionId { get; }
    string? ModalTitle { get; }
    event Action? OnStateChanged;

    Task<WorkflowResponse> StartProcessAsync<T>(IEnumerable<T> items);
    void OpenViewer(string transactionId, string? title = null);
    void CloseViewer();
}
