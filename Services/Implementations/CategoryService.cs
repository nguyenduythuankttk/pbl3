using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services.Implementations{
    public class CategoryService : ICategoryService{
        private readonly AppDbContext _dbcontext;
        public CategoryService (AppDbContext dbContext){
            _dbcontext = dbContext;
        }
        public async Task <List<Category>?> GetAllCategory() =>
            await _dbcontext.Category
            .Include(c => c.Product)
            .ToListAsync();
        public async Task <List<Product>?> GetProductInCategory(int categoryID) =>
            await _dbcontext.Product
            .Where (p => p.CategoryID == categoryID)
            .Include (p => p.Category)
            .ToListAsync();
        public async Task <Category?> GetCategoryByID(int categoryID) =>
            await _dbcontext.Category
                .Include(c => c.Product)
                .Where (c => c.CategoryID == categoryID)
                .FirstOrDefaultAsync();
        public async Task AddCategory(Category newCategory){
            try{
                _dbcontext.Category.Add(newCategory);
                await _dbcontext.SaveChangesAsync();
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task DeleteCategory (Category deleteCategory){
            try {
                _dbcontext.Category.Remove(deleteCategory);
                await _dbcontext.SaveChangesAsync();
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}