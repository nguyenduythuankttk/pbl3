using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models.DTOs.Reponse{
    public class StoreReponse {
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address {get; set;} = null;
    }
}