using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    public enum TableStatus {
        Available,
        Reserved,
        Occupied
    }
    public class DiningTable {
        [Key]
        public int TableID { get; set; }
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; } = null!;
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; } = TableStatus.Available;
        [JsonIgnore]
        public virtual ICollection<Reservation> Reservation { get; set; } = new List<Reservation>();
    }
}
