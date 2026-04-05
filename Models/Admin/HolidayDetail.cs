using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.Admin;

public class HolidayDetail
{
    public int HolidayDetailId { get; set; }

    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public int? HolidayId { get; set; }
    public string? HolidayName { get; set; }

    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public DateTime? FromDate { get; set; } = DateTime.Today;

    [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(HRMS.Resources.AppResources))]
    public DateTime? ToDate { get; set; } = DateTime.Today;

    public int? ShiftTypeId { get; set; }
    public string? ShiftType { get; set; }

    public int? GenderId { get; set; }
    public string? GenderName { get; set; }

    public int? ReligionId { get; set; }
    public string? ReligionName { get; set; }

    public int? NationalityId { get; set; }
    public string? NationalityName { get; set; }

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
