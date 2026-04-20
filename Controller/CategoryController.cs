using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controller
{
    [ApiController]
    [Route("api/pbl3/[controller]")]
    public class categoryController : ControllerBase{
        private readonly ICategoryService _category;
        public  categoryController (ICategoryService category){
            _category = category;
        }
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllCategory(){
        try {
            var categories = await _category.GetAllCategory();
            if (categories == null) return NotFounnd("Not Found Category");
            return OK(categories);
        }
        catch (Exception e){
            return StatusCode(500, "error in Categorycontrolerr.GetAllCategory: " + e.Message); 
        }
    }
    [HttpGet("get/{categoryID}")]
    public async Task<IActionResult> GetCategoryByID(int categoryID){
        try {
            var category = await _category.GetCategoryByID(categoryID);
            if (category == null) return NotFound("Not Found Category ");
            return OK(category);
        } catch (Exception e){
            return StatusCode(500, "error in CategoryController.GetCategoryByID: "+ e.Message);
        }
    }
    [HttpGet("get-all-product in cate {categoryID}")]
    public async Task<IActionResult> GetProductInCategory(int categoryID){
        try {
            var products = await _category.GetProductInCategory(categoryID);
            return OK(products);
        } catch (Exception e){
            return StatusCode(500, "error in CategoryController.GetProductInCategory" + e.Message);
        }
    }
    [HttpPost("addCat")]
    public async Task<IActionResult> AddCategory([FromBody] Category newCategory){
        try{
            await _category.AddCategory(newCategory);
            return OK("Create successfully");
        } catch (Exception e){
            return StatusCode(500, "error in CategoryController.Addcategory" + e.Message);
        }
    }
    [HttpDelete("deleteCat")]
    public async Task<IActionResult> DeleteCategory (int deleteCategoryID){
        try {
            await _category.DeleteCategory(deleteCategoryID);
            return OK("Delete Successfully");
        } catch (Exception e){
            return StatusCode (500, "error in CategoryController.DeleteCate" + e.Message);
        }
    }
    [HttpPut("updateCat")]
    public async Task<IActionResult> Update (int catID, string img){
        try {
            await _category.UpdateCategory(catID,img);
            return OK("Update Category Successfully");
        } catch (Exception e){
            return StatusCode(500, "Error in CategoryController.Update: " + e.Message);
        }
    }
}