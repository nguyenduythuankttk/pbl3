using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models {
    public class Ticket{
        [Key]
        public Guid TicketID {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime EndDate {get; set;}
        public decimal Discount {get; set;}
        public Guid UserID{get; set;}
        [ForeignKey("UserID")]
        public virtual User User {get; set;} = null!;
        
    }
}