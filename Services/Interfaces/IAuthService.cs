using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
namespace Backend.Services.Interface{
    public interface IAuthService{
        Task<(bool emailSent, string? devToken)> Register (RegisterRequest request);
        Task<EmployeeAuthReponse> EmployeeLogin (LoginRequest request);
        Task <UserAuthReponse> UserLogin (LoginRequest request);
        Task Logout (string accessToken);
        Task<UserAuthReponse> ChangePassword(PasswordRequest request, Guid userID);
        Task<(bool emailSent, string? devToken)> ResendVerificationEmail(string email);
        Task VerifyEmail(string token);
        Task<(bool emailSent, string? devToken)> ForgotPassword(string email);
        Task ResetPassword(ResetPasswordRequest request);
    }
}
