namespace HRMS.Models.Master
{
    public class GenericItem
    {
        public int GenericId { get; set; }
        public string GenericCode { get; set; } = "";
        public string GenericTag { get; set; } = "";
        public string GenericName { get; set; } = "";
        public string GenericEn { get; set; } = "";
        public string GenericAr { get; set; } = "";
        public int? GenericOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
