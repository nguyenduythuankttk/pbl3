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

    public class Reservation {
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
        public int TableID { get; set; }

        [ForeignKey("TableID")]
        public virtual DiningTable Table { get; set; } = null!;

        public int GuestCount { get; set; }
        public DateTime ScheduledTime { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
