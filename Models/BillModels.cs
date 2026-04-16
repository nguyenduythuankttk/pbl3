using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public enum PaymentMethods
    {
        Cash,
        Card
    }
    public class Bill
    {
        [Key] //khóa chính
        public Guid BillID { get; set; }

        public Guid UserID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;

        public int StoreID { get; set; }

        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; } = null!;
        public decimal VAT { get; set; }
        public PaymentMethods PaymentMethods { get; set; }
        public decimal Total { get; set; }
        public decimal Paid {get; set;}
        public string? Note { get; set; }
        public decimal MoneyReceived {get; set; }
        public decimal MoneyGiveBack {get; set; }

        [JsonIgnore]
        public virtual ICollection<BillDetail> BillDetail { get; set; } = new List<BillDetail>();

        [JsonIgnore]
        public virtual ICollection<BillChange> BillChange { get; set; } = new List<BillChange>();

    }
}
