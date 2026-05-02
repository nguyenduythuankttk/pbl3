namespace Backend.Models.DTOs.Request{
    public class LoginRequest {
        public string UserName {get; set;} = null!;
        public string HashPassword {get; set;} = null!;
    }
    public class RegisterRequest{
        public string UserName {get; set;} = null!;
        public string HashPassword {get; set;} = null!;
        public string FullName {get; set;} = null!;
        public DateOnly BirthDate {get; set;}
        public string Phone {get; set;} = null!;
        public string Email {get; set;} = null!;
        public Gender Gender {get; set;} 
    }
    public class RefreshRequest{
        public string RefreshToken { get; set; } = null!;
    }
}