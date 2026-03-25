using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Backend.Models {
    public class Supplier{
        [Key]
        public Guid SupplierID{get; set;}
        public string SupplierName{get; set;}
        public Guid AddressID {get;set;}
        [ForeignKey("AddressID")]
        public virtual Address Address {get; set;} = null!;
        public string Phone{get; set;}
        public string Email{get; set;}
        public string TaxCode{get; set;}

    }
}