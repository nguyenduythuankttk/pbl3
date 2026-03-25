using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public enum GoodReceiptStatus
    {
        Preparing,
        Delivering,
        Received
    }

    public class GoodsReceipt
    {
        [Key]
        public Guid GoodsReceiptID { get; set; }

        public Guid EmployeeID { get; set; }

        [ForeignKey(nameof(EmployeeID))]
        public virtual Employee Employee { get; set; } = null!;

        public int StoreID { get; set; }

        [ForeignKey(nameof(StoreID))]
        public virtual Store Store { get; set; } = null!;

        public Guid SupplierID { get; set; }

        [ForeignKey(nameof(SupplierID))]
        public virtual Supplier Supplier { get; set; } = null!;

        public DateTime DateReceive { get; set; }
        public GoodReceiptStatus Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

        [JsonIgnore]
        public virtual ICollection<InventoryBatch> InventoryBatches { get; set; } = new List<InventoryBatch>();
    }
}