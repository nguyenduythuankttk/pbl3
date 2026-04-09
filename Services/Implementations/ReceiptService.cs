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
        public async Task<List<ReceiptItem>> GetAllReceipt() =>
            await _dbContext.Receipt
                .Include(r => r.Employee)
                .Include(r => r.Store)
                .Include(r => r.Supplier)
                .ToListAsync();
    }
}