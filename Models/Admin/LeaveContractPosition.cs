using System;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Models.Admin
{
    public class LeaveContractPosition
    {
        public int LeaveContractPositionId { get; set; }
        
        public int LeaveTypeId { get; set; }
        public string? LeaveTypeName { get; set; }
        
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        
        public int ContractId { get; set; }
        public string? ContractName { get; set; }
        
        public string? CompanyContractName { get; set; }
        
        public int PositionId { get; set; }
        public string? PositionName { get; set; }
        
        public string? CompanyPositionName { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ChangedBy { get; set; }
        public DateTime? ChangedDate { get; set; }
    }
}
