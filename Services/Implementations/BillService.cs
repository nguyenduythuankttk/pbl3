using Backend.Data;
using Backend.Models;
using Backend.Services.Interface;
using Microsoft.EntityFrameWorkCore;
namespace Backend.Services.Implementations{
    public class BillService : IBillService{
        private readonly AppDbContext _dbContext;

        public BillService(AppDbContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<List<Bill>> GetAllBillIn(DateOnly dayStart, DateOnly dateEnd) =>
            await _dbContext.Bill
                .Include(a =>a.BillDetail)
                .Include(a => a.BillModify)
                .Where (a => a.TimeCreated > dayStart.ToDateTime(TimeOnly.MinValue) && a.TimeCreated < dayEnd.ToDateTime(TimeOnly.MaxValue)) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBill(Guid user)   =>
            await _dbContext.Include(a =>a.BillDetail)
                .Include(a => a.BillModify)
                .Where (b => b.User.UserID == user.UserID) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBillSuccess(Guid userID)=>
            await _dbContext.Include(a =>a.BillDetail)
                .Include(a => a.BillModify)
                .Where (b => b.User.UserID == user.UserID) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBillSuccess(Guid userID)=>
            await _dbContext.Include(a =>a.BillDetail)
                .Include(a => a.BillModify)
                .Where (b => b.User.UserID == user.UserID && b.PaymentStatus ==PaymentStatus.Success) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBillPending(Guid userID)=>
            await _dbContext.Include(a =>a.BillDetail)
                .Include(a => a.BillModify)
                .Where (b => b.User.UserID == user.UserID && b.PaymentStatus ==PaymentStatus.Pending) 
                .ToListAsync();
        public async Task<List<Bill>> GetUserBillFail(Guid userID)=>
            await _dbContext.Include(a =>a.BillDetail)
                .Include(a => a.BillModify)
                .Where (b => b.User.UserID == user.UserID && b.PaymentStatus ==PaymentStatus.Fail) 
                .ToListAsync();
        public async Task RemoveBill()
    }
}