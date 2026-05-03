using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
namespace Backend.Services.Interface{
    public interface IAuthService{
        Task Register (RegisterRequest request);
        Task<EmployeeAuthReponse> EmployeeLogin (LoginRequest request);
        Task <UserAuthReponse> UserLogin (LoginRequest request);
        Task Logout (Guid userID, string accessToken); 
    }
}
