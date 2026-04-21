using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class WarehouseService : IAddressService
    {
        private readonly AppDbContext _dbcontext;

        public WarehouseService(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<List<Warehouse>?> GetAllWarehouse() =>
            await _dbcontext.Warehouse
                .AsNoTracking()
                .Include(w => w.Store)
                .ToListAsync();

        public async Task<Warehouse?> GetWarehouseByID(int warehouseID) =>
            await _dbcontext.Warehouse
                .AsNoTracking()
                .Include(w => w.Store)
                .FirstOrDefaultAsync(w => w.WarehouseID == warehouseID);
                
        public async Task<List<Warehouse>?> GetWarehouseByStore(int storeID) =>
            await _dbcontext.Warehouse
                .AsNoTracking()
                .Where(w => w.StoreID == storeID)
                .Include(w => w.Store)
                .ToListAsync();

        
    }
}