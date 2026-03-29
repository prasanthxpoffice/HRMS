namespace HRMS.Models.Workflow;

public class WorkflowResponse
{
    public string? TransactionId { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
}
