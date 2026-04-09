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
        [Key] //khóa chính
        public Guid BillID { get; set; }

        public Guid UserID { get; set; }

        [ForeignKey(nameof(UserID))] //khóa phụ đến bảng user 
        public virtual User User { get; set; } = null!; // Mối quan hệ 1 - n giữa bill và user 

        public int StoreID { get; set; }

        [ForeignKey(nameof(StoreID))]
        public virtual Store Store { get; set; } = null!;

        public decimal VAT { get; set; }
        public PaymentMethods PaymentMethods { get; set; }
        public PaymentStatus PaymentStatus {get; set;}
        public DateTime TimeCreated { get; set; }
        public decimal Total { get; set; }
        public bool IsDeleted {get; set; } =false;

        [JsonIgnore]
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();
    }
}
