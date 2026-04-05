using System;

namespace HRMS.Models.Master;

public class ShiftType
{
    public int ShiftTypeId { get; set; }
    public string? ShiftTypeName { get; set; }
    public string? ShiftTypeEn { get; set; }
    public string? ShiftTypeAr { get; set; }
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
