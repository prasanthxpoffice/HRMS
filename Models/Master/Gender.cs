namespace HRMS.Models.Master;
public class Gender
{
    public int GenderId { get; set; }
    public string GenderNameEn { get; set; } = "";
    public string GenderNameAr { get; set; } = "";
    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
