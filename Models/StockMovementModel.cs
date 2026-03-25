using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public enum StockMovementType
    {
        PurchaseReceipt,
        Consumption,
        Waste,
        TransferIn,
        TransferOut,
        ManualAdjustment,
        AuditAdjustment
    }

    public enum StockReferenceType
    {
        GoodsReceipt,
        Bill,
        StockAudit,
        Transfer,
        Manual
    }

    public class StockMovement
    {
        [Key]
        public Guid StockMovementID { get; set; }

        public int WarehouseID { get; set; }

        [ForeignKey(nameof(WarehouseID))]
        public virtual Warehouse Warehouse { get; set; } = null!;

        public int IngredientID { get; set; }

        [ForeignKey(nameof(IngredientID))]
        public virtual Ingredient Ingredient { get; set; } = null!;

        public Guid? BatchID { get; set; }

        [ForeignKey(nameof(BatchID))]
        public virtual InventoryBatch? Batch { get; set; }

        public Guid EmployeeID { get; set; }

        [ForeignKey(nameof(EmployeeID))]
        public virtual Employee Employee { get; set; } = null!;

        public decimal QtyChange { get; set; }
        public StockMovementType MovementType { get; set; }
        public StockReferenceType ReferenceType { get; set; }
        public string? ReferenceID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string? Reason { get; set; }
        public string? Note { get; set; }
    }
}