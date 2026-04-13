namespace Backend.Models.DTOs.Request
{
    public class StoreCreateRequest
    {
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        
        // public int TotalPoints { get; set; }
        public int SeatingCapacity { get; set; }
    }
    
    public class StoreUpdateRequest
    {

        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
  
        public int SeatingCapacity { get; set; }

        // Quang còn thắc mắc nếu muốn update address thì sao 
    }
}