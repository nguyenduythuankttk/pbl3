using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controller
{
    [ApiController]
    [Route("api/pbl3/[controller]")]
    public class authController : ControllerBase {
        private readonly IAuthService _AuthService;
        private readonly IUserService _UserService;
        private readonly IEmployeeService _EmployeeSevice;

        public authController (
            IAuthService authService,
            IUserService userService,
            IEmployeeService employeeService
        ){
            _AuthService = authService;
            _EmployeeSevice = employeeService;
            _UserService = userService;
        }

        [HttpPost("customer_login")]
        public async Task<IActionResult> CustomerLogin([FromBody] LoginRequest request){
            try{
                var reponse = await _AuthService.UserLogin(request);
                if (reponse == null) {
                    return Unauthorized("Sai tên đăng nhập hoặc mật khẩu");
                }
                return Ok(new { message = "Đăng nhập thành công!", data = reponse });
            } catch (InvalidOperationException e){
                return BadRequest(new { message = e.Message });
            } catch (Exception){
                return StatusCode(500, "Error in authController.Login");
            }
        }

        [HttpPost("employee_login")]
        public async Task<IActionResult> EmployeeLogin([FromBody] LoginRequest request){
            try{
                var reponse = await _AuthService.EmployeeLogin(request);
                if (reponse == null) {
                    return Unauthorized("Sai tên đăng nhập hoặc mật khẩu");
                }
                return Ok(new { message = "Đăng nhập thành công!", data = reponse });
            } catch (Exception){
                return StatusCode(500, "Error in authController.Login");
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(){
            try{
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _AuthService.Logout(token);
                return Ok("Đăng xuất thành công");
            }catch (Exception){
                return StatusCode(500, "Error in authController.logout");
            }
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser(){
            try{
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(userID)) return Unauthorized();
                var user = await _UserService.GetUserByID(Guid.Parse(userID));
                return Ok(user);
            }catch(Exception){
                return StatusCode(500, "Error in authController.getcurrentuser");
            }
        }

        [Authorize]
        [HttpGet("me/employee")]
        public async Task<IActionResult> GetCurrentEmployee(){
            try{
                var empID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(empID)) return Unauthorized();
                var emp = await _EmployeeSevice.GetEmployeeByID(Guid.Parse(empID));
                return Ok(emp);
            }catch(Exception){
                return StatusCode(500, "Error in authController.GetCurrentEmployee");
            }
        }

        /// <summary>Đăng ký tài khoản và gửi email xác thực</summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request){
            try{
                var (emailSent, devToken) = await _AuthService.Register(request);
                return Ok(new {
                    message = emailSent
                        ? "Đăng ký thành công. Vui lòng kiểm tra email để xác thực tài khoản."
                        : "Đăng ký thành công nhưng không thể gửi email. Hãy dùng endpoint /resend-verify-email.",
                    emailSent,
                    devToken  // null ở production, có giá trị ở Development để test
                });
            } catch (InvalidOperationException e){
                return Conflict(new { message = e.Message });
            } catch (Exception e){
                return StatusCode(500, new { message = "Error in authController.Register: " + e.Message });
            }
        }

        /// <summary>Gửi lại email xác thực</summary>
        [HttpPost("resend-verify-email")]
        public async Task<IActionResult> ResendVerifyEmail([FromBody] ForgotPasswordRequest request){
            try{
                var (emailSent, devToken) = await _AuthService.ResendVerificationEmail(request.Email);
                return Ok(new {
                    message = emailSent ? "Email xác thực đã được gửi lại." : "Không thể gửi email. Vui lòng kiểm tra cấu hình Resend.",
                    emailSent,
                    devToken
                });
            } catch (InvalidOperationException e){
                return BadRequest(new { message = e.Message });
            } catch (Exception){
                return StatusCode(500, "Error in authController.ResendVerifyEmail");
            }
        }

        /// <summary>Xác thực email sau khi nhấn link trong mail</summary>
        [HttpGet("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token){
            try{
                await _AuthService.VerifyEmail(token);
                return Ok(new { message = "Email đã được xác thực thành công. Bạn có thể đăng nhập ngay bây giờ." });
            } catch (InvalidOperationException e){
                return BadRequest(new { message = e.Message });
            } catch (Exception){
                return StatusCode(500, "Error in authController.VerifyEmail");
            }
        }

        /// <summary>Yêu cầu gửi email đặt lại mật khẩu</summary>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request){
            try{
                var (emailSent, devToken) = await _AuthService.ForgotPassword(request.Email);
                return Ok(new {
                    message = emailSent
                        ? "Email đặt lại mật khẩu đã được gửi. Vui lòng kiểm tra hộp thư."
                        : "Không thể gửi email. Vui lòng kiểm tra cấu hình Resend.",
                    emailSent,
                    devToken
                });
            } catch (InvalidOperationException e){
                return BadRequest(new { message = e.Message });
            } catch (Exception){
                return StatusCode(500, "Error in authController.ForgotPassword");
            }
        }

        /// <summary>Đặt lại mật khẩu bằng token từ email</summary>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request){
            try{
                await _AuthService.ResetPassword(request);
                return Ok(new { message = "Mật khẩu đã được đặt lại thành công." });
            } catch (InvalidOperationException e){
                return BadRequest(new { message = e.Message });
            } catch (Exception){
                return StatusCode(500, "Error in authController.ResetPassword");
            }
        }

        /// <summary>Đổi mật khẩu khi đã đăng nhập</summary>
        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] PasswordRequest request){
            try{
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(userID)) return Unauthorized();
                var result = await _AuthService.ChangePassword(request, Guid.Parse(userID));
                return Ok(new { message = "Đổi mật khẩu thành công.", data = result });
            } catch (Exception e){
                return StatusCode(500, new { message = "Error in authController.ChangePassword: " + e.Message });
            }
        }
    }
}
