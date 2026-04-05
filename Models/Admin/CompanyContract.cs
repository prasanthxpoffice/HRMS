using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.Admin;

public class CompanyContract
{
    public int CompanyContractId { get; set; }

    [Required(ErrorMessageResourceName = "CompanyContract_Company_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public int? CompanyId { get; set; }
    
    public string? CompanyName { get; set; }

    [Required(ErrorMessageResourceName = "CompanyContract_Contract_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public int? ContractId { get; set; }
    
    public string? ContractName { get; set; }

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
