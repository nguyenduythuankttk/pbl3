/*using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services.Implementations{
    public class BillService : IBillService{
        private readonly AppDbContext _dbContext;

        public BillService(AppDbContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<List<Bill>> GetAllBillIn(DateOnly dayStart, DateOnly dayEnd) =>
            await _dbContext.Bill
                .Include(a => a.BillDetail)
                .Include(a => a.ProductVarient)
                .Include(a => a.Product)
                .Where (a => a.TimeCreated > dayStart.ToDateTime(TimeOnly.MinValue) && a.TimeCreated < dayEnd.ToDateTime(TimeOnly.MaxValue)) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBill(Guid userID)   =>
            await _dbContext.Bill.Include(a =>a.BillDetail)
                .Include(a => a.ProductVarient)
                .Include(a => a.Product)
                .Where (b => b.User.UserID == userID) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBillSuccess(Guid userID)=>
            await _dbContext.Bill.Include(a =>a.BillDetail)
                .Include(a => a.ProductVarient)
                .Include(a => a.ProductVarient)
                .Where (b => b.User.UserID == user) 
                .ToListAsync();

        public async Task<List<Bill>> GetUserBillPending(Guid userID)=>
            await _dbContext.Bill.Include(a =>a.BillDetail)
                .Include(a => a.ProductVarient)
                .Include(a => a.ProductVarient)
                .Where (b => b.User.UserID == UserID && b.PaymentStatus == PaymentStatus.Pending) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBillFail(Guid userID)=>
            await _dbContext.Bill.Include(a =>a.BillDetail)
                .Include(a => a.ProductVarient)
                .Include(a => a.ProductVarient)
                .Where (b => b.User.UserID == UserID && b.PaymentStatus == PaymentStatus.Fail) 
                .ToListAsync();
        public async Task RemoveBill(Guid billID){
            try {
                var bill = _dbContext.Bill.FirstOrDefaultAsync(b => b.BillID ==billID);
                if (bill != null){
                    _dbContext.Bill.Remove(bill);
                }
                await _dbContext.SaveChangeAsync();
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task DeleteBill (Guid b){
            try {
                var bill = _dbContext.Bill.FirstOrDefaultAsync(a => a.BillID == b);
                if (bill != null){
                    bill.IsDeleted = true;
                }
                await _dbContext.SaveChangeAsync();
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task<Bill?> GetBillByID(Guid billID) => 
            await _dbContext.Bill
            .Include(b => b.BillDetail)
            .Include(b => b.ProductVarient)
            .Include(b => b.Product)
            .FirstOrDefaultAsync(b => b.BillID == billID);
        public async Task AddBill(Bill bill){
            try {
                _dbContext.Bill.Add(bill);
                await _dbContext.SaveChangeAsync();
            } 
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}*/