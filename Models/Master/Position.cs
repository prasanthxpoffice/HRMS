using System.ComponentModel.DataAnnotations;
using HRMS.Resources;

namespace HRMS.Models.Master;

public class Position
{
    public int PositionId { get; set; }
    public string? PositionName { get; set; }

    [Required(ErrorMessageResourceName = nameof(AppResources.Position_NameEn_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string PositionNameEn { get; set; } = "";

    [Required(ErrorMessageResourceName = nameof(AppResources.Position_NameAr_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string PositionNameAr { get; set; } = "";

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
