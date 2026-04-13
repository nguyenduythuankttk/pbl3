using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models{
    public class BillChange{
        [Key]
        public Guid BillChangeID { get; set;}
        public Guid BillID{ get; set;}
        [ForeignKey("BillID")]
        public virtual Bill Bill {get; set;} = null!;
        public DateTime CreateAt {get; set;}
        public decimal AmountPaid { get; set;}
    }
}