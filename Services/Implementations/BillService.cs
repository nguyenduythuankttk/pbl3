// using Backend.Data;
// using Backend.Models;
// using Backend.Services.Interface;
// using Microsoft.EntityFrameworkCore
// namespace Backend.Services.Implementations{
//     public class BillService : IBillService{
//         private readonly AppDbContext _dbContext;

//         public BillService(AppDbContext dbContext){
//             _dbContext = dbContext;
//         }
//         public async Task<List<Bill>> GetAllBillIn(DateOnly dayStart, DateOnly dateEnd) =>
//             await _dbContext.Bill
//                 .Include(a => a.BillDetail)
//                 .Include(a => a.ProductVarient)
//                 .Where (a => a.TimeCreated > dayStart.ToDateTime(TimeOnly.MinValue) && a.TimeCreated < dayEnd.ToDateTime(TimeOnly.MaxValue)) 
//                 .ToListAsync();
//         public async Task<List<Bill>> GetUserBill(Guid user)   =>
//             await _dbContext.Include(a =>a.BillDetail)
//                 .Include(a => a.ProductVarient)
//                 .Where (b => b.User.UserID == user) 
//                 .ToListAsync();
//         public async Task<List<Bill>> GetUserBillSuccess(Guid userID)=>
//             await _dbContext.Include(a =>a.BillDetail)
//                 .Include(a => a.ProductVarient)
//                 .Where (b => b.User.UserID == user) 
//                 .ToListAsync();
//         public async Task<List<Bill>> GetUserBillSuccess(Guid userID)=>
//             await _dbContext.Include(a =>a.BillDetail)
//                 .Include(a => a.ProductVarient)
//                 .Where (b => b.User.UserID == UserID && b.PaymentStatus == PaymentStatus.Success) 
//                 .ToListAsync();
//         public async Task<List<Bill>> GetUserBillPending(Guid userID)=>
//             await _dbContext.Include(a =>a.BillDetail)
//                 .Include(a => a.ProductVarient)
//                 .Where (b => b.User.UserID == UserID && b.PaymentStatus == PaymentStatus.Pending) 
//                 .ToListAsync();
//         public async Task<List<Bill>> GetUserBillFail(Guid userID)=>
//             await _dbContext.Include(a =>a.BillDetail)
//                 .Include(a => a.ProductVarient)
//                 .Where (b => b.User.UserID == UserID && b.PaymentStatus == PaymentStatus.Fail) 
//                 .ToListAsync();
//         public async Task RemoveBill(Guid billID){
//             try {
//                 var bill = _dbContext.Bill.FirstOrDefaultAsync(b => b.BillID ==billID);
//                 if (bill != null){
//                     _dbContext.Bill.Remove(bill);
//                 }
//                 await _dbContext.SaveChangeAsync();
//             }
//             catch (Exception e){
//                 Console.WriteLine(e.Message);
//             }
//         }
//         public async Task DeleteBill (BillDeleteRequest b){
            
//         }
//     }
// }