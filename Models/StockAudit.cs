using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public enum AuditStatus
    {
        Draft,
        InProgress,
        Completed,
        Approved,
        Cancelled
    }

    public class StockAudit
    {
        [Key]
        public Guid StockAuditID { get; set; }

        public int WarehouseID { get; set; }

        [ForeignKey(nameof(WarehouseID))]
        public virtual Warehouse Warehouse { get; set; } = null!;

        public DateTime CreateAt { get; set; }
        public Guid CreateBy { get; set; }

        [ForeignKey(nameof(CreateBy))]
        public virtual Employee CreateByEmployee { get; set; } = null!;

        public Guid? ApprovedBy { get; set; }

        [ForeignKey(nameof(ApprovedBy))]
        public virtual Employee? ApprovedByEmployee { get; set; }

        public string? Note { get; set; }
        public AuditStatus Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<StockAuditDetail> StockAuditDetails { get; set; } = new List<StockAuditDetail>();
    }
}