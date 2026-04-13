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
<<<<<<< HEAD

        public Guid AddressID { get; set; }

        [ForeignKey("AddressID")]
=======
>>>>>>> 3f996c8133d0c3e2b659425e1aa1cdd644fb15df
        public virtual Address Address { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

        [JsonIgnore]
        public virtual ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();

        [JsonIgnore]
        public virtual ICollection<DiningTable> DiningTables { get; set; } = new List<DiningTable>();
    }
}