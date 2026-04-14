using Backend.Data;
using Backend.Models;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services.Implementations{
    public class BillSevice : IBillService{
        private readonly AppDbContext _dbcontext;
        public BillSevice (AppDbContext dbcontext){
            _dbcontext = dbcontext;
        }

        public async Task <List<Bill?>> GetAllBillIn(DateOnly start, DateOnly end) =>
            await _dbcontext.Bill
            .Include (b => b.BillDetail)
                .ThenInclude(bd => bd.ProductVarient)
                    .ThenInclude(pv => pv.Product)
            .Where (b => b.CreateAt > start.ToDateTime(TimeOnly.MinValue) && b.CreateAt > start.ToDateTime(TimeOnly.MinValue))
            .ToListAsync();
        public async Task <List<Bill>?> GetUserBill(Guid userID) =>
        
    }
}