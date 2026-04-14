using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public enum IngredientUnit
    {
        Unit,
        Gram,
        Kilogram,
        Liter,
        Milliliter
    }

    public class Ingredient
    {
        [Key]
        public int IngredientID { get; set; }

        public string IngredientName { get; set; } = null!;
        public IngredientUnit IngredientUnit { get; set; }
        public decimal CostPerUnit { get; set; }

        [JsonIgnore]
        public virtual ICollection<PODetail> PODetail { get; set; } = new List<PODetail>();

        [JsonIgnore]
        public virtual ICollection<ReceiptDetail> ReceiptDetail { get; set; } = new List<ReceiptDetail>();

        [JsonIgnore]
        public virtual ICollection<InventoryBatch> InventoryBatche { get; set; } = new List<InventoryBatch>();

        [JsonIgnore]
        public virtual ICollection<Receipe> Recipe { get; set; } = new List<Receipe>();

        [JsonIgnore]
        public virtual ICollection<StockMovement> StockMovement { get; set; } = new List<StockMovement>();
    }
}