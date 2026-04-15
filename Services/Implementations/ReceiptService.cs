using Backend.Models;
using Backend.Models.DTOs;
using Backend.Data;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class ReceiptService : IReceiptService
    {
        private readonly AppDbContext _dbContext;

        public ReceiptService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET ALL
        public async Task<List<Receipt>> GetAllReceipt() =>
            await _dbContext.Receipt
                .Include(r => r.Employee)
                .Include(r => r.Store)
                .Include(r => r.Supplier)
                .ToListAsync();
        
        public async Task<Receipt?> GetReceiptbyID(Guid goodsReceiptID) =>
            await _dbContext.Receipt    
                .Include(r => r.Employee)
                .Include(r => r.Store)
                    .ThenInclude(c => c.Address)
                .Include(r => r.PurchaseOrder)
                .Include(r => r.Supplier)
                    .ThenInclude(c => c.Address)
                .FirstOrDefaultAsync(r => r.GoodsReceiptID == goodsReceiptID);
        
        public async Task<List<Receipt>> GetReceiptbyPO(Guid pOID) =>
            await _dbContext.Receipt
                .Include(r => )

        



                
    }

}