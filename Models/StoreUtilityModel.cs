using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Backend.Models {
    public class StoreUtility{
        public int UtilityID {get; set;}
        [ForeignKey("UtilityID")] 
        public virtual Utility Utility {get; set;} = null!;
        public int StoreID {get;set;}
        [ForeignKey("StoreID")]
        public virtual Store Store {get; set;} =null!;
        public DateOnly Date {get; set;}
        public double Quantity {get; set;}
    }
    
}