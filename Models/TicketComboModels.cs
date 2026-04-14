using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{
    public class TicketCombo{
        public int ComboID {get; set;}
        [ForeignKey("ComboID")]
        public virtual Combo Combo {get; set;} = null!;
        public Guid TicketID {get; set;}
        [ForeignKey("TicketID")]
        public virtual Ticket Ticket {get; set;} = null!;
    
    }
}