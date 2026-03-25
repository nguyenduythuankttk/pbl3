using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {

    public class User{
        [Key]
        public Guid UserID {get; set;}
        public string UserName {get; set;}
        public string Password {get; set;}
        public DateOnly BirthDate {get; set;}
        public DateTime CreateAt {get; set;}
        public string Email {get; set;}
        public string Phone {get; set;}
        public string FullName {get; set;}

        [JsonIgnore]
        public virtual ICollection<UserAddress> UserAddress { get; set; } = new List<UserAddress>();

        [JsonIgnore]
        public virtual ICollection<Ticket> Ticket{get; set;} = new List<Ticket>();
    }       
}