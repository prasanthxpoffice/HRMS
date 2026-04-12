using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRMS.Models.Policy
{
    public class LeavePolicy
    {
        public int LeavePolicyId { get; set; }

        public string? LeaveContractPositionIds { get; set; }

        [Required]
        public DateTime PolicyFromDate { get; set; } = DateTime.Now;

        [Required]
        public DateTime PolicyToDate { get; set; } = new DateTime(2099, 12, 31);

        public bool IsProbationRequired { get; set; } = true;

        public bool IsCreditOnHireAnniversary { get; set; } = true;

        [JsonPropertyName("isCreditInAdvance")]
        public bool IsCreditInAdvance { get; set; } = false;

        [Range(1, 28)]
        public int? CreditOnDay { get; set; }

        public int? CreditOnMonth { get; set; }

        [Required]
        [Range(0, 9999)]
        public decimal FrequencyInDays { get; set; } = 365;

        [Required]
        [Range(0, 999)]
        public decimal TotalCredit { get; set; }

        public int? GenderId { get; set; }

        public int? ReligionId { get; set; }

        public bool IsActive { get; set; } = true;

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
