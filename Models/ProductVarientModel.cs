using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{
    public enum ProductSize {
        Default, S, M, L, XL
    }
    public class ProductVarient{
        [Key]
        public int ProductVarientID {get; set;}
        public int ProductID {get; set;}
        [ForeignKey("ProductID")]
        public virtual Product Product {get; set;} = null!;

        public ProductSize Size {get; set;} = ProductSize.Default;
        public decimal Price {get; set;}
        
        [JsonIgnore]
<<<<<<< HEAD
        public virtual ICollection<ComboProduct> ComboProduct {get; set;} = new List<ComboProduct>();
=======
        public virtual ICollection<ComboProduct> ComboProduct {get; set; } = new List<ComboProduct>();
>>>>>>> 3f996c8133d0c3e2b659425e1aa1cdd644fb15df
    }
}