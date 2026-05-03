using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Services.Implementations{
    public class AuthService : IAuthService{
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext dbContext,
                           IPasswordHasher<User> passwordHasher,
                           IConfiguration configuration){
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        private string GenerateAcessToken(User user){
            try{
                var jwtKey = _configuration["Jwt:Key"] ?? throw new Exception("JWT Key is not configured");
                var issuer = _configuration["Jwt:Issuer"] ?? throw new Exception("JWT Issuer is not configured");
                var audience = _configuration["Jwt:Audience"] ?? throw new Exception("JWT Audience is not configured");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim(ClaimTypes.Role, user is Employee emp ? emp.Role.ToString() : "Customer")
                };
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            } catch (Exception e){
                Console.WriteLine(e.Message);
                throw new Exception("Can't generate access token");
            }
        }

        public async Task Register(RegisterRequest request){
            try{
                bool isExisted = await _dbContext.User.AnyAsync(u => request.Email == u.Email ||
                                                                     request.Phone == u.Phone);
                if (isExisted)
                    throw new Exception("Email hoặc số điện thoại đã tồn tại");

                isExisted = await _dbContext.User.AnyAsync(u => u.UserName == request.UserName);
                if (isExisted)
                    throw new Exception("Đã tồn tại UserName");

                var newCustomer = new User{
                    UserName = request.UserName,
                    HashPassword = request.HashPassword,
                    FullName = request.FullName,
                    BirthDate = request.BirthDate,
                    Phone = request.Phone,
                    Email = request.Email,
                    Gender = request.Gender
                };
                newCustomer.HashPassword = _passwordHasher.HashPassword(newCustomer, request.HashPassword);
                _dbContext.User.Add(newCustomer);
                await _dbContext.SaveChangesAsync();
            } catch (Exception e){
                Console.WriteLine(e.Message);
                throw new Exception("Can't Register");
            }
        }

        public async Task<EmployeeAuthReponse> EmployeeLogin(LoginRequest request){
            try{
                var emp = await _dbContext.Employee
                    .FirstOrDefaultAsync(e => e.UserName == request.UserName);
                if (emp == null)
                    throw new Exception("Không tìm thấy nhân viên");

                var verify = _passwordHasher.VerifyHashedPassword(emp, emp.HashPassword, request.HashPassword);
                if (verify == PasswordVerificationResult.Failed)
                    throw new Exception("Sai tên đăng nhập hoặc mật khẩu");

                var accessToken = GenerateAcessToken(emp);
                await _dbContext.SaveChangesAsync();

                return new EmployeeAuthReponse{
                    AcessToken = accessToken,
                    EmployeeID = emp.UserID,
                    EmployeeName = emp.FullName,
                    Phone = emp.Phone,
                    Email = emp.Email,
                    StoreID = emp.StoreID,
                    Role = emp.Role,
                    FullName = emp.FullName,
                    BirthDate = emp.BirthDate,
                    BasicSalary = emp.BasicSalary
                };
            } catch (Exception e){
                Console.WriteLine(e.Message);
                throw new Exception("Error in EmployeeLogin");
            }
        }

        public async Task<UserAuthReponse> UserLogin(LoginRequest request){
            try{
                var usr = await _dbContext.User
                    .FirstOrDefaultAsync(e => e.UserName == request.UserName);
                if (usr == null)
                    throw new Exception("Không tìm thấy người dùng");

                var verify = _passwordHasher.VerifyHashedPassword(usr, usr.HashPassword, request.HashPassword);
                if (verify == PasswordVerificationResult.Failed)
                    throw new Exception("Sai tên đăng nhập hoặc mật khẩu");

                var accessToken = GenerateAcessToken(usr);
                await _dbContext.SaveChangesAsync();

                return new UserAuthReponse{
                    AcessToken = accessToken,
                    UserID = usr.UserID,
                    UserName = usr.UserName,
                    Phone = usr.Phone,
                    Email = usr.Email,
                    FullName = usr.FullName,
                    BirthDate = usr.BirthDate
                };
            } catch (Exception e){
                Console.WriteLine(e.Message);
                throw new Exception("Error in UserLogin");
            }
        }
        
        public async Task Logout(Guid userID, string accessToken){
            try{
                var user = await _dbContext.User.FirstOrDefaultAsync(u => u.UserID == userID);
                if (user == null)
                    throw new Exception("Không tìm thấy user");

                if (!string.IsNullOrEmpty(accessToken)){
                    _dbContext.BlackListedToken.Add(new BlacklistedToken{
                        Token = accessToken,
                        ExpiryDate = DateTime.UtcNow.AddMinutes(15)
                    });
                }
                await _dbContext.SaveChangesAsync();
            } catch (Exception e){
                Console.WriteLine(e.Message);
                throw new Exception("Error in Logout");
            }
        }

    }
}
