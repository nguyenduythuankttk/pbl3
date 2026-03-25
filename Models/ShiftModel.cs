using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    public class Shift{
        [Key]
        public Guid ShiftID {get; set;}
        public Guid EmployeeID {get; set;}
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee {get; set;} = null!;
        public DateTime TimeIn {get; set;}
        public DateTime TimeOut {get; set;}
        public DateTime ChechIn {get; set;} 
        public DateTime CheckOut {get; set;}

    }
}