using Backend.Models;
using Backend.Models.DTOs;
namespace Backend.Services.Interface{
    public interface ICategoryService{
        Task <List<Category>?> GetAllCategory();
        Task <List<Product>?> GetProductInCategory(int categoryID);
        Task <Category?> GetCategoryByID(int categoryID);
        Task AddCategory(Category newCategory);
        Task DeleteCategory (Category deleteCategory);
    }
}
