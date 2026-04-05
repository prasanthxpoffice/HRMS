using System;

namespace HRMS.Models.Master;

public class Religion
{
    public int ReligionId { get; set; }
    public string? ReligionName { get; set; }
    public string? ReligionNameEn { get; set; }
    public string? ReligionNameAr { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
