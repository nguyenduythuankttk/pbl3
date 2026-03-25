using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public enum BatchStatus
    {
        Available,
        Depleted,
        Expired,
        Damaged,
        Locked
    }

    public class InventoryBatch
    {
        [Key]
        public Guid BatchID { get; set; }

        public int WarehouseID { get; set; }

        [ForeignKey(nameof(WarehouseID))]
        public virtual Warehouse Warehouse { get; set; } = null!;

        public DateTime ImportDate { get; set; }
        public DateOnly Exp { get; set; }
        public DateOnly Mfd { get; set; }
        public decimal QuantityOriginal { get; set; }
        public decimal QuantityOnHand { get; set; }
        public BatchStatus Status { get; set; }
        public decimal UnitCost { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? BatchCode { get; set; }
        public string? Note { get; set; }

        public int IngredientID { get; set; }

        [ForeignKey(nameof(IngredientID))]
        public virtual Ingredient Ingredient { get; set; } = null!;

        public Guid GoodsReceiptID { get; set; }
        public virtual ReceiptDetail ReceiptDetail { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<StockAuditDetail> StockAuditDetails { get; set; } = new List<StockAuditDetail>();
    }
}