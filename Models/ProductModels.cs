using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public enum ProductType
    {
        Combo,
        Food,
        Drink
    }

    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; } = null!;
        public ProductType ProductType { get; set; }
        public DateTime ProductionTime { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }

        [JsonIgnore]
        public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

        [JsonIgnore]
        public virtual ICollection<Receipe> Recipes { get; set; } = new List<Receipe>();
    }
}