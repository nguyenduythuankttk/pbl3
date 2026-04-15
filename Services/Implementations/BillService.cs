using Backend.Data;
using Backend.Models;
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
            .Include (b => b.BillDetail)
                .ThenInclude(bd => bd.ProductVarient)
                    .ThenInclude(pv => pv.Product)
            .Include(b => b.BillChange)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Where (b => b.CreateAt > start.ToDateTime(TimeOnly.MinValue) && b.CreateAt < end.ToDateTime(TimeOnly.MaxValue))
            .ToListAsync();
        public async Task <List<Bill>?> GetUserBill(Guid userID) => 
            await _dbcontext.Bill
            .Include (b => b.BillDetail)
                .ThenInclude (bd => bd.ProductVarient)
                    .ThenInclude (pr => pr.Product)
            .Include (b => b.BillChange)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Where (b => b.UserID == userID)
            .ToListAsync();
        public async Task<List<Bill>?> GetPaidBill() =>
            await _dbcontext.Bill
            .Include (b => b.BillDetail)
                .ThenInclude (bd => bd.ProductVarient)
                    .ThenInclude (pr => pr.Product)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Include (b => b.BillChange)
            .Where(b => b.BillChange
                .OrderByDescending(bc => bc.ChangeAt)
                .Select(bc => bc.Status)
                .FirstOrDefault() == BillStatus.Paid)
            .ToListAsync();
        public async Task<List<Bill>?> GetUnPaidBill() =>
            await _dbcontext.Bill
            .Include (b => b.BillDetail)
                .ThenInclude (bd => bd.ProductVarient)
                    .ThenInclude (pr => pr.Product)
            .Include (b => b.BillChange)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Where(b => b.BillChange
                .OrderByDescending(bc => bc.ChangeAt)
                .Select(bc => bc.Status)
                .FirstOrDefault() == BillStatus.UnPaid)
            .ToListAsync();
        public async Task<List<Bill>?> GetDeletedBill() =>
            await _dbcontext.Bill
            .Include (b => b.BillDetail)
                .ThenInclude (bd => bd.ProductVarient)
                    .ThenInclude (pr => pr.Product)
            .Include (b => b.BillChange)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Where(b => b.BillChange
                .OrderByDescending(bc => bc.ChangeAt)
                .Select(bc => bc.Status)
                .FirstOrDefault() == BillStatus.Delete)
            .ToListAsync();
        public async Task <List<Bill>?> GetCashBill() =>
            await _dbcontext.Bill
            .Include(b => b.BillDetail)
                    .ThenInclude (bd => bd.ProductVarient)
                        .ThenInclude (pr => pr.Product)
            .Include (b => b.BillChange)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Where(b => b.PaymentMethods == PaymentMethods.Cash)
            .ToListAsync();
        public async Task <List<Bill>?> GetCardBill() =>
            await _dbcontext.Bill
            .Include(b => b.BillDetail)
                    .ThenInclude (bd => bd.ProductVarient)
                        .ThenInclude (pr => pr.Product)
            .Include (b => b.BillChange)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Where(b => b.PaymentMethods == PaymentMethods.Card)
            .ToListAsync();
        public async Task<Bill?> GetBillByID(Guid billID) =>
            await _dbcontext.Bill
            .Include(b => b.BillDetail)
                .ThenInclude (bd => bd.ProductVarient)
                    .ThenInclude (pr => pr.Product)
            .Include (b => b.Employee)
            .Include (b => b.Store)
            .Where(b => b.BillID == billID)
            .FirstOrDefaultAsync();
        public async Task AddBill(Bill bill){
            try {
                _dbcontext.Bill.Add(bill);
                await _dbcontext.SaveChangesAsync();
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task SetPaid(Guid billID){
            var bill = await _dbcontext.Bill.FindAsync(billID);
            if (bill == null) return;
            var change = new BillChange {
                BillChangeID = Guid.NewGuid(),
                BillID = billID,
                Status = BillStatus.Paid,
                ChangeAt = DateTime.UtcNow
            };
            _dbcontext.BillChange.Add(change);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task DeleteBill(Guid billID){
            var bill = await _dbcontext.Bill.FindAsync(billID);
            if (bill == null) return;
            var change = new BillChange {
                BillChangeID = Guid.NewGuid(),
                BillID = billID,
                Status = BillStatus.Delete,
                ChangeAt = DateTime.UtcNow
            };
            _dbcontext.BillChange.Add(change);
            await _dbcontext.SaveChangesAsync();
        }
    }
}