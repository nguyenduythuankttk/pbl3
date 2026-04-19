using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Backend.Models.DTOs.Request{
    public class ShiftCreateRequest {
        Guid EmployeeID {get; set;}
        DateTime TimeIn {get; set;}
        DateTime TimeOut {get; set;}
    }
    public class ShiftUpdateRequest {
        DateTime? CheckIn {get; set;} = DateTime.UtcNow;
        DateTime? CheckOut {get; set;} = DateTime.UtcNow;
    }
}