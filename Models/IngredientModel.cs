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

        public string Name { get; set; } = null!;
        public IngredientUnit IngredientUnit { get; set; }
        public decimal CostPerUnit { get; set; }

        [JsonIgnore]
        public virtual ICollection<PODetail> PODetails { get; set; } = new List<PODetail>();

        [JsonIgnore]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

        [JsonIgnore]
        public virtual ICollection<InventoryBatch> InventoryBatches { get; set; } = new List<InventoryBatch>();

        [JsonIgnore]
        public virtual ICollection<Receipe> Recipes { get; set; } = new List<Receipe>();

        [JsonIgnore]
        public virtual ICollection<StockAuditDetail> StockAuditDetails { get; set; } = new List<StockAuditDetail>();
    }
}