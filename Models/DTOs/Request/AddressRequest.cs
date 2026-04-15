using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models.DTOs.Request{
    public class AddressCreateRequest{
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
    }
}