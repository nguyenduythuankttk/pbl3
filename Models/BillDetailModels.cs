using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{

    public class BillDetail
    {
        public Guid BillID { get; set; }

        [ForeignKey(nameof(BillID))]
        public virtual Bill Bill { get; set; } = null!;

        public int ProductID { get; set; }

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; } = null!;

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal InlineTotal { get; set; }
    }
}