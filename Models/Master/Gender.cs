using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.Master;

public class Gender
{
    public int GenderId { get; set; }

    [Required(ErrorMessageResourceName = "Gender_NameEn_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = "Error_TooLong", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string GenderNameEn { get; set; } = "";

    [Required(ErrorMessageResourceName = "Gender_NameAr_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    [StringLength(100, ErrorMessageResourceName = "Error_TooLong", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public string GenderNameAr { get; set; } = "";

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
