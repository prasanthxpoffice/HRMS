using System.ComponentModel.DataAnnotations;
using HRMS.Resources;

namespace HRMS.Models.Master;

public class Holiday
{
    public int HolidayId { get; set; }
    public string? HolidayName { get; set; }

    [Required(ErrorMessageResourceName = nameof(AppResources.Holiday_NameEn_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string HolidayNameEn { get; set; } = "";

    [Required(ErrorMessageResourceName = nameof(AppResources.Holiday_NameAr_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string HolidayNameAr { get; set; } = "";

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
