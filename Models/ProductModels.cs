using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public enum ProductType
    {
        Food,
        Drink,
        Addon
    }

    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; } = null!;
        [Required]
        public string ProductName { get; set; } = null!;
        [Required]
        public ProductType ProductType { get; set; }
        public string? Image { get; set; }
        public DateTime? DeleteAt {get; set;}
        [JsonIgnore]
        public virtual ICollection<ProductVarient> ProductVarient { get; set; } = new List<ProductVarient>();


    }
}