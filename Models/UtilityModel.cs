using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models {
    public enum UtilityType{
        CubicMeter,
        kWh,
        kg
        
    }
    public class Utility{
        [Key]
        public int UtilityID {get; set;}
        public UtilityType Type {get; set;}
        public decimal Cost{get; set;}
        [JsonIgnore]
        public virtual ICollection<StoreUtility> StoreUtility {get; set;} = new List<StoreUtility>();
        
    }
}