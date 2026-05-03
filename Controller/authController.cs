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
            } catch (Exception e){
                return StatusCode(500,"Error in authController.Login");
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
            } catch (Exception e){
                return StatusCode(500,"Error in authController.Login");
            }
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(){
            try{
                var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                await _AuthService.Logout(token);
                return Ok("Đăng xuất thành công");
            }catch (Exception e){
                return StatusCode(500,"Error in authController.logout");
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
            }catch(Exception e){
                return StatusCode(500,"Error in authController.getcurrentuser");
            }
        }
        [Authorize]
        [HttpGet("me/employee")]
        public async Task<IActionResult> GetCurrentEmployee(){
            try{
                var empID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrWhiteSpace(empID)) return Unauthorized();
                var emp = await _EmployeeSevice.GetEmployeeByID();
                return Ok(emp);
            }catch(Exception e){
                return StatusCode(500,"Error in authController.GetCurrentEmployee");
            }
        }

    }
}