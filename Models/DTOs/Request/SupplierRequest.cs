namespace Backend.Models.DTOs.Request
{
    public class SupplierUpdateRequest
    {
        public string? SupplierName{get; set;}
        public string? Phone{get; set;}
        public string? Email{get; set;}
        public string? TaxCode{get; set;}

    }
}