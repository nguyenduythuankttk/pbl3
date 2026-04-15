using Backend.Data;
using Backend.Models;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly AppDbContext _dbContext;
        public StoreService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Store>?> GetAllStore() =>
            await _dbContext.Store
                .Include(s => s.Address)
                .ToListAsync();

        public async Task<Store?> GetStoreByID (int store) => 
            await _dbContext.Store
                .Include(s => s.Adress)
                .FirstOrDefaultAsync(s => s.StoreID == StoreID);

        public async Task<List<Store?>> GetbyAddress(Guid addressID) => 
            await _dbContext.Store
                .Include(s => Adress)
                .where(s => s.AddressID = addressID)
                .ToListAsync();




    }
}