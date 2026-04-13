using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public enum PaymentMethods
    {
        Cash,
        Card,
        QR
    }
    public class Bill
    {
        [Key]
        public Guid BillID { get; set; }

        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;

        public int StoreID { get; set; }

        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; } = null!;
        public Guid EmployeeID {get; set;}
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; } = null!;

        public decimal VAT { get; set; }
        public PaymentMethods PaymentMethods { get; set; }
        public DateTime TimeCreated { get; set; }
        public decimal Total { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid? DeletedBy {get; set;}
        [ForeignKey("DeletedBy")]
        public Employee DeletedByEmployee {get; set;}
        public decimal Paid {get; set;}
        public string? Note { get; set; }

        [JsonIgnore]
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

        [JsonIgnore]
        public virtual ICollection<BillChange> BillChanges { get; set; } = new List<BillChange>();

    }
}
