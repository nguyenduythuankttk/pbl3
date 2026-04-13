using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models.DTOs.Request{
    public class ComboRequest{
        public int ComboID {get; set;}
        public string ComboName {get; set;}
        public decimal FixedPrice {get; set;}
        public bool IsActive {get; set;}
    }
}