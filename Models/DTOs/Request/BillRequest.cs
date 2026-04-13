using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models.DTOs.Request{
    public class BillDeleteRequest{
        public bool IsDeleted {get; set;}
    }
}
