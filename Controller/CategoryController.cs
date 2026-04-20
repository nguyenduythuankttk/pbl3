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
    public class CategoryController : ControllerBase{
        private readonly ICategoryService _category;
        public  CategoryController (ICategoryService category){
            _category = category;
        }
    }
    [Http("get-all")]
    public async Task<IActionResult> GetAllCategory(){
        try{
            var res = 
        }
        catch (Exception e){

        }
    }
}