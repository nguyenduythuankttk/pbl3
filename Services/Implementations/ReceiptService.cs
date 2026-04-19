using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
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
        public async Task<List<Receipt>?> GetAllReceiptIn(DateOnly start, DateOnly end) =>
            await _dbContext.Receipt
                .Include(r => r.Employee)
                .Include(r => r.Store)
                .Include(r => r.Supplier)
                .Where (b => b.DateReceive > start.ToDateTime(TimeOnly.MinValue) && b.DateReceive < end.ToDateTime(TimeOnly.MaxValue))
                .ToListAsync();
        
        public async Task<Receipt?> GetReceiptByID(Guid goodsReceiptID) =>
            await _dbContext.Receipt    
                .Include(r => r.Employee)
                .Include(r => r.Store)
                    .ThenInclude(c => c.Address)
                .Include(r => r.PurchaseOrder)
                .Include(r => r.Supplier)
                    .ThenInclude(c => c.Address)
                .FirstOrDefaultAsync(r => r.GoodsReceiptID == goodsReceiptID);
        
        public async Task<List<Receipt>?> GetReceiptByPO(Guid pOID) =>
            await _dbContext.Receipt
                .Include(r => r.Employee)
                .Include(r => r.Store)
                    .ThenInclude(c => c.Address)
                .Include(r => r.PurchaseOrder)
                .Include(r => r.Supplier)
                    .ThenInclude(c => c.Address)
                .Where(r => r.POID == pOID)
                .ToListAsync();

        public async Task<List<Receipt>?> GetReceiptByStore(int storeID) =>
            await _dbContext.Receipt
                .Include(r => r.Employee)
                .Include(r => r.Store)
                    .ThenInclude(c => c.Address)
                .Include(r => r.PurchaseOrder)
                .Include(r => r.Supplier)
                    .ThenInclude(c => c.Address)
                .Where(r => r.StoreID == storeID)
                .ToListAsync();

        public async Task<List<Receipt>?> GetReceiptByEmployee(Guid employeeID) =>
            await _dbContext.Receipt
                .Include(r => r.Employee)
                .Include(r => r.Store)
                    .ThenInclude(c => c.Address)
                .Include(r => r.PurchaseOrder)
                .Include(r => r.Supplier)
                    .ThenInclude(c => c.Address)
                .Where(r => r.EmployeeID == employeeID)
                .ToListAsync();

        public async Task<List<Receipt>?> GetReceiptBySupplier(Guid supplierID) =>
            await _dbContext.Receipt
                .Include(r => r.Employee)
                .Include(r => r.Store)
                    .ThenInclude(c => c.Address)
                .Include(r => r.PurchaseOrder)
                .Include(r => r.Supplier)
                    .ThenInclude(c => c.Address)
                .Where(r => r.SupplierID == supplierID)
                .ToListAsync();
        
        public async Task AddReceipt(Receipt receipt){
            try {
                _dbContext.Receipt.Add(receipt); // thêm Bill vào bảng Bill trong db
                await _dbContext.SaveChangesAsync(); //chờ lưu tất cả thay đổi vào db
            }catch (Exception e){
                Console.WriteLine(e.Message);
            }

        // tam thoi Q chua lam set trong receipt nha

        }




        



                
}

}