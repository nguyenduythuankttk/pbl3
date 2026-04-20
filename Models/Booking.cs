using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models {
    public enum BookingStatus {
        Pending,
        Confirmed,
        Cancelled,
        Completed,
        Noshow
    }

    public class Booking {
        [Key]
        public Guid BookingID {get; set;}
        [Required]
        public DateTime ScheduledTime { get; set; }
        [Required, Range(1, 100)]
        public int NumberOfGuess {get; set; } 
        public string? GuestComment {get; set; }
        public DateTime CreateAt {get; set; }
        public DateTime? DeletedAt {get; set; }
        public BookingStatus BookingStatus {get; set; }
        public Guid UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; } = null!;
        public int TableID { get; set; }

        [ForeignKey("TableID")]
        public virtual DiningTable Table { get; set; } = null!;

        public virtual BookingApproval BookingApproval {get; set; } = null!;
       
    }
}
