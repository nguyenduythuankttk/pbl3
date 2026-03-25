using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models{

    public class Address
    {
        [Key]
        public Guid AddressID { get; set; }

        public int? HouseNumber { get; set; }

        [Required, MaxLength(200)]
        public string Street { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Ward { get; set; } = null!;

        [Required, MaxLength(200)]
        public string District { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Province { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Country { get; set; } = "Viet Nam";

        public virtual Store? Store { get; set; }
        public virtual Supplier? Supplier { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
    }
}