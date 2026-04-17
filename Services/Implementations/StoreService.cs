using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Request;
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

        public async Task<Store?> GetStoreByID (int storeID) => 
            await _dbContext.Store
                .Include(s => s.Address)
                .FirstOrDefaultAsync(s => s.StoreID == storeID);

        public async Task<List<Store>?> GetStoreByAdress(Guid addressID) => 
            await _dbContext.Store
                .Include(s => s.Address)
                .Where(s => s.AddressID == addressID)
                .ToListAsync();//chuyeren kieu du lieu sang list

        public async Task AddStore(Store store)
        {
            try
            {
                //Thêm store
                _dbContext.Store.Add(store);
                await _dbContext.SaveChangesAsync();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateStore(int storeID, StoreUpdateRequest request)
        {
            var store = await _dbContext.Store.FindAsync(storeID); //Thuận set trong db là Store

            if(store == null)
                throw new Exception("Store not found");
            try
            {
                store.Phone = request.Phone; //gắn vào ram tạm
                store.Email = request.Email;
                store.SeatingCapacity = request.SeatingCapacity;

                await _dbContext.SaveChangesAsync();
            } catch (Exception ex)
            {
                Console.WriteLine($"Update Store Error: {ex.Message}");
                throw new Exception($"An error occurred while updating the store: {ex.Message}");
            }

        }

        public async Task DeleteStore(int storeID)
        {
            var store = await _dbContext.Store.FindAsync(storeID);
            if(store == null) return;
            store.IsActive = false;
            await _dbContext.SaveChangesAsync();
        }



    }
}

