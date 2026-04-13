using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    // Ghi lại lịch sử thay đổi trạng thái từng bước trong quá trình giao hàng
    public class DeliveryLog {
        [Key]
        public Guid LogID { get; set; }
        public Guid DeliveryID { get; set; }
        [ForeignKey("DeliveryID")]
        public virtual DeliveryInfo DeliveryInfo { get; set; } = null!;
        public Guid EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; } = null!;
        public DeliveryStatus FromStatus { get; set; } // Trạng thái trước khi thay đổi
        public DeliveryStatus ToStatus { get; set; } // Trạng thái sau khi thay đổi
        public DateTime ChangedAt { get; set; }
        public string? Note { get; set; }
    }
}
