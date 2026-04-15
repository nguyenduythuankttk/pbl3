using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models.DTOs.Request{
    public class DeliveryInfoCreateRequest{
        public Guid BillID { get; set; }
        [ForeignKey("BillID")]
        public virtual Bill Bill { get; set; } = null!;
        public Guid UserID {get; set;}
        [ForeignKey("UserID")]
        public Guid AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; } = null!;
        public DateTime? CreateAt { get; set; }  
        public decimal ShippingFee { get; set; }
        public string? Note { get; set; }
    }
}