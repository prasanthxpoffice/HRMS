namespace HRMS.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StoredProcedureAttribute : Attribute
    {
        public string? Get { get; set; }
        public string? Save { get; set; }
        public string? Delete { get; set; }
    }
}
