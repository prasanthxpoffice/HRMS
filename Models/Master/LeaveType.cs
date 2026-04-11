using System.ComponentModel.DataAnnotations;
using HRMS.Resources;
using System.Text.Json.Serialization;

namespace HRMS.Models.Master;

public class LeaveType
{
    public int LeaveTypeId { get; set; }
    public string? LeaveTypeName { get; set; }
    [Required(ErrorMessageResourceName = nameof(AppResources.LeaveType_NameEn_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string LeaveTypeEn { get; set; } = "";

    [Required(ErrorMessageResourceName = nameof(AppResources.LeaveType_NameAr_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string LeaveTypeAr { get; set; } = "";

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
