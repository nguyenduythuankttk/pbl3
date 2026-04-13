using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models{
    public class Combo{
        [Key]
        public int ComboID {get; set;}
        public string ComboName {get; set;}
        public decimal FixedPrice {get; set;}
        public bool IsActive{get; set;}
        [JsonIgnore]
        public virtual ICollection<ComboProduct> ComboProduct {get; set;} = new List<ComboProduct>();
    }
}