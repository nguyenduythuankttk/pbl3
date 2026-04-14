using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{
    public class TicketProduct{
        public int ProductVarientID {get; set;}
        [ForeignKey("ProductVarientID")]
        public virtual ProductVarient ProductVarient {get; set;} = null!;
        public Guid TicketID {get; set;}
        [ForeignKey("TicketID")]
        public virtual Ticket Ticket {get; set;} = null!;
    
    }
}