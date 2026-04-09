namespace Backend.Models.DTOs
{
    public class ReceiptResponse
    {
        public List<ReceiptItem> Results {get; set; } = new();
    }

    public class ReceiptItem
    {
        public Guid ReceiptID { get; set; }

        public Guid EmployeeID { get; set; }
        public int StoreID { get; set; }
        public Guid SupplierID { get; set; }
        public DateTime DateReceive { get; set; }
        public ReceiptStatus Status { get; set; }
    }
}