using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public class PurchaseOrder
    {
        [Key]
        public Guid POID { get; set; }

        public int StoreID { get; set; }

        [ForeignKey(nameof(StoreID))]
        public virtual Store Store { get; set; } = null!;

        public Guid SupplierID { get; set; }

        [ForeignKey(nameof(SupplierID))]
        public virtual Supplier Supplier { get; set; } = null!;

        public Guid EmployeeID { get; set; }

        [ForeignKey(nameof(EmployeeID))]
        public virtual Employee Employee { get; set; } = null!;

        [Required]
        public DateTime SentAt { get; set; }

        [Required]
        public decimal TaxRate { get; set; }

        [Required]
        public DateTime ReceivedDate { get; set; }

        [JsonIgnore]
        public ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();

        [JsonIgnore]
        public virtual ICollection<POApproval> POApprovals { get; set; } = new List<POApproval>();
    }
}