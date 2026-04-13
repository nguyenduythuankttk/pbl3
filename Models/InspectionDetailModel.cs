using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    public enum DamageReason {
        Damaged,
        Expired,
        WrongSpec,
        Shortage
    }

    public class InspectionDetail {
        public Guid InspectionID {get; set;}

        [ForeignKey("InspectionID")]
        public virtual GoodsInspection GoodsInspection { get; set; } = null!;
        public int IngredientID { get; set; }

        [ForeignKey("IngredientID")]
        public virtual Ingredient Ingredient { get; set; } = null!;
        public decimal ReceivedQty { get; set; }

        // Số lượng đạt chất lượng
        public decimal GoodQty { get; set; }

        // Số lượng hư hỏng → ghi StockMovement Waste
        public decimal DamagedQty { get; set; }

        // Số lượng hết hạn
        public decimal ExpiredQty { get; set; }

        // Số lượng thiếu so với đơn hàng
        public decimal ShortageQty { get; set; }

        public DamageReason? DamageReason { get; set; }
        public string? Note { get; set; }
    }
}
