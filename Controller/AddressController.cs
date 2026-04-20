using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // Lấy UserID từ JWT claim, trả về null nếu không hợp lệ
        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var id) ? id : null;
        }

        // GET api/v1/address/my
        // Lấy tất cả địa chỉ của người dùng đang đăng nhập
        [HttpGet("my")]
        public async Task<IActionResult> GetMyAddresses()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var user = new User { UserID = userId.Value };
            var addresses = await _addressService.GetUserAddress(user);
            return Ok(addresses);
        }

        // GET api/v1/address/get/{addressId}
        // Lấy 1 địa chỉ cụ thể — chỉ trả về nếu thuộc về người dùng hiện tại
        [HttpGet("get/{addressId:guid}")]
        public async Task<IActionResult> GetAddress(Guid addressId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var address = await _addressService.GetAddressByID(addressId);
            if (address == null) return NotFound();

            // Kiểm tra quyền sở hữu: địa chỉ phải liên kết với user hiện tại
            bool isOwner = address.UserAddresses?.Any(ua => ua.UserID == userId.Value) ?? false;
            if (!isOwner) return Forbid();

            return Ok(address);
        }

        // POST api/v1/address/add
        // Thêm địa chỉ mới và gắn với người dùng hiện tại
        [HttpPost("add")]
        public async Task<IActionResult> AddAddress([FromBody] AddressCreateRequest request)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            var address = new Address
            {
                AddressID = Guid.NewGuid(),
                HouseNumber = request.HouseNumber,
                Street = request.Street,
                Ward = request.Ward,
                District = request.District,
                Province = request.Province,
                Country = request.Country
            };

            await _addressService.AddUserAddress(address, userId.Value);
            return Ok(new { message = "Thêm địa chỉ thành công.", addressId = address.AddressID });
        }

        // DELETE api/v1/address/delete/{addressId}
        // Xóa địa chỉ — chỉ xóa địa chỉ thuộc về người dùng hiện tại
        [HttpDelete("delete/{addressId:guid}")]
        public async Task<IActionResult> DeleteAddress(Guid addressId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            await _addressService.DeleteUserAddress(addressId, userId.Value);
            return Ok(new { message = "Xóa địa chỉ thành công." });
        }

        // PUT api/v1/address/setdefault/{addressId}
        // Đặt địa chỉ mặc định — chỉ áp dụng cho địa chỉ thuộc về người dùng hiện tại
        [HttpPut("setdefault/{addressId:guid}")]
        public async Task<IActionResult> SetDefault(Guid addressId)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return Unauthorized();

            await _addressService.SetDefault(addressId, userId.Value);
            return Ok(new { message = "Đặt địa chỉ mặc định thành công." });
        }
    }
}
