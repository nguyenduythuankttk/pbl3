using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public enum ApprovalStatus
    {
        Pending,
        Accepted,
        Rejected
    }

    public class BookingApproval
    {
        [Key]
        public Guid ApprovalID {get; set; }
        public ApprovalStatus ApprovalStatus {get; set; }
        [Required]
        public string RejectionReason {get; set; } = null!;
        public string? comment {get; set; }
        public DateTime ApprovalTime {get; set; } 
        [ForeignKey("BookingID")]
        public virtual Booking Booking {get; set; } = null!;
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee {get; set; } = null!;
    }
}