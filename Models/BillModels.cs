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
    public enum PaymentStatus{
        Success,
        Pending,
        Fail
    }

    public class Bill
    {
        [Key]
        public Guid BillID { get; set; }

        public Guid UserID { get; set; }

        [ForeignKey(nameof(UserID))]
        public virtual User User { get; set; } = null!;

        public int StoreID { get; set; }

        [ForeignKey(nameof(StoreID))]
        public virtual Store Store { get; set; } = null!;

        public decimal VAT { get; set; }
        public PayMentmethods PaymentMethods { get; set; }
        public PaymentStatus PaymentStatus {get; set;}
        public DateTime TimeCreated { get; set; }
        public decimal Total { get; set; }
        public bool IsDeleted {get; set; } =false;

        [JsonIgnore]
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
    }
}
