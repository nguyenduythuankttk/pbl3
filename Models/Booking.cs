using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    public enum ReservationStatus {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    public class Booking {
        [Key]
        public Guid BookingID {get; set;}
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
        public int TableID { get; set; }

        [ForeignKey("TableID")]
        public virtual DiningTable Table { get; set; } = null!;
        [Required]
        public DateTime ScheduledTime { get; set; }
    }
}
