namespace HRMS.Models;

public class Menu
{
    public string MenuCode { get; set; } = "";
    public string? ParentMenuCode { get; set; }
    public string MenuNameEn { get; set; } = "";
    public string MenuNameAr { get; set; } = "";
    public string Href { get; set; } = "";
    public string Icon { get; set; } = "";
    public int SortOrder { get; set; }
}
