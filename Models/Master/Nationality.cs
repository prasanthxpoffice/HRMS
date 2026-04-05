using System;

namespace HRMS.Models.Master;

public class Nationality
{
    public int NationalityId { get; set; }
    public string? NationalityName { get; set; }
    public string? NationalityNameEn { get; set; }
    public string? NationalityNameAr { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
