using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;namespace Backend.Models;
namespace Backend.Models{
    public enum ProductSize {
        Default, S, M, L, XL
    }
    public class ProductVarient{
        [Key]
        public int ID {get; set;}
        public int ProductID {get; set;}
        [ForeignKey("ProductID")]
        public virtual Product Product {get; set;} = null!;
        public ProductSize Size {get; set;} = ProductSize.Default;
        public decimal Price {get; set;}
        [JsonIgnore]
        public virtual ICollection<ComboProduct> ComboProduct = new List<ComboProduct>();
    }
}