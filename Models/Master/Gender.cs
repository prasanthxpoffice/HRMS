using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.Master;

public class Gender
{
    public int GenderId { get; set; }

    [Required(ErrorMessage = "English Name is required")]
    [StringLength(100, ErrorMessage = "English Name is too long")]
    public string GenderNameEn { get; set; } = "";

    [Required(ErrorMessage = "Arabic Name is required")]
    [StringLength(100, ErrorMessage = "Arabic Name is too long")]
    public string GenderNameAr { get; set; } = "";

    public int? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ChangedBy { get; set; }
    public DateTime? ChangedDate { get; set; }
}
