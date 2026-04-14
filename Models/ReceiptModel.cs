using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public enum ReceiptStatus
    {
        Preparing,
        Delivering,
        Received
    }

    public class Receipt
    {
        [Key]
        public Guid GoodsReceiptID { get; set; }
        public Guid EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; } = null!;
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; } = null!;
        public Guid SupplierID { get; set; }

        [ForeignKey("SupplierID")]
        public virtual Supplier Supplier { get; set; } = null!;

        public Guid POID { get; set; }

        [ForeignKey("POID")]
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public virtual GoodsInspection? GoodsInspection { get; set; }

        public DateTime DateReceive { get; set; }
        public ReceiptStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid DeletedBy { get; set; }
        [ForeignKey("DeletedBy")]
        public virtual Employee Deleted { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
    }
}