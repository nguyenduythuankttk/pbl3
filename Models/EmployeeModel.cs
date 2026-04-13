using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public enum RoleType
    {
        Manager,
        Dining,
        Kitchen,
        Counter
    }

    public class Employee : User
    {
        public RoleType Role { get; set; }
        public int StoreID { get; set; }
        [ForeignKey(nameof(StoreID))]
        public virtual Store Store { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
        [JsonIgnore]
        public virtual ICollection<POApproval> POApprovals { get; set; } = new List<POApproval>();
        [JsonIgnore]
        public virtual ICollection<Receipt> GoodsReceipts { get; set; } = new List<Receipt>();

        [JsonIgnore]
        public virtual ICollection<GoodsInspection> GoodsInspections { get; set; } = new List<GoodsInspection>();
        [JsonIgnore]
        public virtual ICollection<DeliveryLog> DeliveryLogs { get; set; } = new List<DeliveryLog>();
        [JsonIgnore]
        public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
        public decimal BasicSalary { get; set; }
    }
}