using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    public enum DeliveryStatus {
        Pending,        
        Confirmed,   
        Preparing,  
        OnTheWay,   
        Delivered,   
        Cancelled,
        Failed     
    }

    public class DeliveryInfo {
        [Key]
        public Guid DeliveryID { get; set; }
        public Guid BillID { get; set; }
        [ForeignKey("BillID")]
        public virtual Bill Bill { get; set; } = null!;
        public Guid UserID {get; set;}
        [ForeignKey("UserID")]
        public Guid AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; } = null!;
        public DateTime? EstimatedTime { get; set; }   // Dự kiến giao lúc
        public DateTime? ActualTime { get; set; }       // Thực tế giao lúc
        public decimal ShippingFee { get; set; }
        public DeliveryStatus CurrentStatus { get; set; } = DeliveryStatus.Pending;
        public string? Note { get; set; }

        [JsonIgnore]
        public virtual ICollection<DeliveryLog> DeliveryLog { get; set; } = new List<DeliveryLog>();
    }
}
