using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.Admin;

public class CompanyPosition
{
    public int CompanyPositionId { get; set; }
    
    [Required(ErrorMessageResourceName = "CompanyPosition_Company_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public int? CompanyId { get; set; }
    
    public string? CompanyName { get; set; }
    public string? CompanyNameEn { get; set; }
    public string? CompanyNameAr { get; set; }

    [Required(ErrorMessageResourceName = "CompanyPosition_Position_Required", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public int? PositionId { get; set; }
    
    public string? PositionName { get; set; }
    public string? PositionNameEn { get; set; }
    public string? PositionNameAr { get; set; }

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
