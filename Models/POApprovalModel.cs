using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public enum PO_Status
    {
        Submitted,
        Approved,
        Rejected,
        Ordered,
        Received,
        Cancelled
    }

    public class POApproval
    {
        public Guid POID { get; set; }

        [ForeignKey(nameof(POID))]
        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;

        public Guid EmployeeID { get; set; }

        [ForeignKey(nameof(EmployeeID))]
        public virtual Employee Employee { get; set; } = null!;

        public DateTime LastUpdated { get; set; }
        public string? Comment { get; set; }
        public PO_Status Status { get; set; }
    }
}