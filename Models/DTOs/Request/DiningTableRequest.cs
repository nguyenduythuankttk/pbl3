using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models.DTOs.Request{
    public class BookingTable {
        public Guid UserID { get; set; }
        public int TableID { get; set; }
        public DateTime ScheduledTime { get; set; }
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public string? Note { get; set; }
        public DateTime ChangeAt { get; set; }
    }
} 