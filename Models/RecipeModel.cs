using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public class Receipe
    {
        public int IngredientID { get; set; }

        [ForeignKey(nameof(IngredientID))]
        public virtual Ingredient Ingredient { get; set; } = null!;

        public int ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; } = null!;

        public decimal Quantity { get; set; }
        public decimal QtyBeforeProcess { get; set; }
        public decimal QtyAfterProcess { get; set; }
    }
}