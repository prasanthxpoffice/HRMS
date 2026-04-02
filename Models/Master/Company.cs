using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.Master;

public class Company
{
    public int CompanyId { get; set; }

    [Required(ErrorMessageResourceName = "Company_NameEn_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = "Error_TooLong", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string CompanyNameEn { get; set; } = "";

    [Required(ErrorMessageResourceName = "Company_NameAr_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = "Error_TooLong", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string CompanyNameAr { get; set; } = "";

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}