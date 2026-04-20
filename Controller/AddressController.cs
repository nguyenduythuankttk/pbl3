using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.AspNetCore.Mvc;
namespace Backend.Controller{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AddressController : ControllerBase{
        private readonly IAddressService _addressService;
        public AddressController (IAddressService addressService){
            _addressService = addressService;
        }
        [HttpGet("get/{addressID}")]
        
    }
}