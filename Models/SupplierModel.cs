using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models {
    public class Supplier{
        [Key]
        public int SupplierID{get; set;}
        [Required]
        public string SupplierName{get; set;}
        public Guid AddressID {get; set;}
        [ForeignKey("AddressID")]
        public virtual Address Address {get; set;} = null!;
        [Required]
        public string Phone{get; set;}
        [Required]
        public string Email{get; set;}
        [Required]
        public string TaxCode{get; set;}

    }
}