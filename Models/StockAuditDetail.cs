using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public class StockAuditDetail
    {
        public Guid StockAuditID { get; set; }

        [ForeignKey(nameof(StockAuditID))]
        public virtual StockAudit StockAudit { get; set; } = null!;

        public Guid BatchID { get; set; }

        [ForeignKey(nameof(BatchID))]
        public virtual InventoryBatch InventoryBatch { get; set; } = null!;

        public decimal SysQty { get; set; }
        public decimal CounterQty { get; set; }
        public decimal Difference { get; set; }
        public string? Note { get; set; }
    }
}