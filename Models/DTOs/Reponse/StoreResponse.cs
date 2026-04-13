namespace Backend.Models.DTOs.Reponse
{
    public class StoreResponse //nay cho user
    { //toi thi dung dto, truoc do thi dung implementation
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int TotalReviews { get; set; }
        public int TotalPoints { get; set; }
    }
}