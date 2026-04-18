using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {

    public class User{
        [Key]
        public Guid UserID {get; set;}
        [Required]
        public string UserName {get; set;}
        [Required]
        public string Password {get; set;}
        [Required]
        public DateOnly BirthDate {get; set;}
        [Required]
        public DateTime CreateAt {get; set;}
        [Required]
        public string Email {get; set;}
        [Required]
        public string Phone {get; set;}
        [Required]
        public string FullName {get; set;}
        [JsonIgnore]
        public virtual ICollection<UserAddress> UserAddress { get; set; } = new List<UserAddress>();
        [JsonIgnore]
        public virtual ICollection<Ticket> Ticket { get; set; } = new List<Ticket>();
        [JsonIgnore]
        public virtual ICollection<Reservation> Reservation { get; set; } = new List<Reservation>();
    }       
}