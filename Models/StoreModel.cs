using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{

    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int TotalReviews { get; set; }
        public int TotalPoints { get; set; }
        public int SeatingCapacity { get; set; }

        public Guid AddressID { get; set; }

        [ForeignKey(nameof(AddressID))]
        public virtual Address Address { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        [JsonIgnore]
        public virtual ICollection<StoreUtility> StoreUtilities { get; set; } = new List<StoreUtility>();

        [JsonIgnore]
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    }
}