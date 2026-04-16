using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
public enum BillStatus{
    Create,
    Paid,
    UnPaid,
    Delete
}
namespace Backend.Models{
    public class BillChange{
        [Key]
        public Guid BillChangeID { get; set;}
        public Guid BillID{ get; set;}
        [ForeignKey("BillID")]
        public Guid EmployeeID {get; set;}
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee {get; set;} = null!;
        public virtual Bill Bill {get; set;} = null!;
        public DateTime ChangeAt {get; set;}
        public BillStatus Status {get; set;}
    }
}