using System.ComponentModel.DataAnnotations;
using HRMS.Resources;

namespace HRMS.Models.Master;

public class ContractType
{
    public int ContractId { get; set; }
    public string? ContractName { get; set; }

    [Required(ErrorMessageResourceName = nameof(AppResources.ContractType_NameEn_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string ContractNameEn { get; set; } = "";

    [Required(ErrorMessageResourceName = nameof(AppResources.ContractType_NameAr_Required), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = nameof(AppResources.Error_TooLong), ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string ContractNameAr { get; set; } = "";

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
