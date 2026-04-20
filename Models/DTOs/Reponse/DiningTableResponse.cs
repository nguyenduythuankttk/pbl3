namespace Backend.Models.DTOs.Reponse {
    public class DiningTableResponse {
        public int TableID { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; } = null!;
        public string StorePhone { get; set; } = null!;
        public string StoreEmail { get; set; } = null!;
        public int PendingReservations { get; set; }
    }
    public class TableListResponse {
        public int StoreID { get; set; }
        public int TotalTables { get; set; }
        public int AvailableTables { get; set; }
        public List<DiningTableResponse> Tables { get; set; } = new();
    }
}
