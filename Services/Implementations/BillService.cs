using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services.Implementations{
    public class BillService : IBillService{
        private readonly AppDbContext _dbcontext;
        public BillService (AppDbContext dbcontext){
            _dbcontext = dbcontext;
        }

        public async Task <List<Bill>?> GetAllBillIn(DateOnly start, DateOnly end) =>
            await _dbcontext.Bill
                    .AsNoTracking()
                    .Include (b => b.BillChange
                    .OrderBy(bc =>bc.ChangeAt)
                    .Take(1))
                    .ThenInclude(bc => bc.Employee)
                    .Where( b => b.BillChange.Any() &&
                            b.BillChange.Max(b => b.ChangeAt) >= start.ToDateTime(TimeOnly.MinValue) &&
                            b.BillChange.Max(b => b.ChangeAt) <= end.ToDateTime(TimeOnly.MinValue))
                    .Include(b => b.BillDetail)
                        .ThenInclude(bd => bd.ProductVarient)
                            .ThenInclude(pr => pr.Product)
                    .Include(b => b.Store)
                    .ToListAsync();
                    
        public async Task <List<Bill>?> GetUserBill(Guid userID) => 
            await _dbcontext.Bill
            .AsNoTracking()
            .Where (b => b.UserID == userID)
            .Include (b => b.BillDetail)
                .ThenInclude (bd => bd.ProductVarient)
                    .ThenInclude (pr => pr.Product)
            .Include (b => b.BillChange.OrderByDescending(bc => bc.ChangeAt))
            .Include (b => b.Store)
            .ToListAsync();
        public async Task<Bill?> GetBillByID(Guid billID) =>
            await _dbcontext.Bill
            .AsNoTracking()
            .Where(b => b.BillID == billID)
            .Include(b => b.BillDetail)
                .ThenInclude (bd => bd.ProductVarient)
                    .ThenInclude (pr => pr.Product)
            .Include (b => b.BillChange.OrderByDescending(bc => bc.ChangeAt))
                .ThenInclude (b => b.Employee)
            .Include (b => b.Store)
            .FirstOrDefaultAsync();
        public async Task AddBill(BillCreateRequest request){
            var newBill = new Bill{
                UserID = request.UserID,
                StoreID = request.StoreID,
                VAT = request.VAT,
                PaymentMethods = request.PaymentMethods,
                Total = request.Total,
                Paid = request.Paid,
                Note = request.Note,
                MoneyGiveBack = request.MoneyGiveBack,
                MoneyReceived = request.MoneyReceived
            };
            var newChange = new BillChange{
                BillID = newBill.BillID,
                EmployeeID = request.EmployeID,
                ChangeAt = DateTime.UtcNow,
                Status = BillStatus.Create
            };
            try {
                _dbcontext.Bill.Add(newBill);
                _dbcontext.BillChange.Add(newChange);
                await _dbcontext.SaveChangesAsync();
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task ChangeBill(BillChangeRequest changeRequest){
            var newChange = new BillChange {
                BillID = changeRequest.BillID,
                Status = changeRequest.Status,
                ChangeAt = changeRequest.ChangeAt
            };
            _dbcontext.BillChange.Add(newChange);
            await _dbcontext.SaveChangesAsync();
        }
    }
}