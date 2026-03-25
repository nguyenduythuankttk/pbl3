using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public enum PayMentMethods
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

        [ForeignKey(nameof(UserID))]
        public virtual User User { get; set; } = null!;

        public int StoreID { get; set; }

        [ForeignKey(nameof(StoreID))]
        public virtual Store Store { get; set; } = null!;

        public decimal VAT { get; set; }
        public PayMentMethods PaymentMethods { get; set; }
        public DateTime TimeCreated { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
    }
}
