using Backend.Data;
using Backend.Models;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
namespace Backend.Services.Implementations{
    public class DeliveryService : IDeliveryInfoService{
        private readonly AppDbContext _dbcontext;
        public DeliveryService (AppDbContext dbContext){
            _dbcontext = dbContext;
        }
        public async Task <List<DeliveryInfo>?> GetAllDeliveryIn (DateTime start, DateTime end) =>
            await _dbcontext.DeliveryInfo
                .Include(di => di.DeliveryLog)
                .OrderBy(Log => Log.ChangeAt)
                    .ThenInclude(Log => Log.Employee)
                .Where (di => di.CreateAt > start.ToDateTime(TimeOnly.MinValue) && di.CreateAt < end.ToDateTime(TimeOnly.MaxValue))
                .ToListAsync();
        public async Task <List<DeliveryInfo>?> GetAllDeliveryByUser() =>
            await _dbcontext.DeliveryInfo
                .Include(di => di.DeliveryLog)
                .OrderBy(Log => Log.ChangeAt)
                    .ThenInclude(Log => Log.Employee)
                .Where (di => di.UserID ==userID)
                .ToListAsync();
        public async Task AddDeliveryInfo(DeliveryInfoCreateRequest request){
            try {
                var delivery = new DeliveryInfo{
                    BillID = request.BillID,
                    UserID = request.UserID,
                    AddressID = request.AddressID,
                    CreateAt = request.CreateAt,
                    ShippingFee = request.ShippingFee,
                    Note = request.Note
                };
                _dbcontext.DeliveryLog.Add(log);
                await _dbcontext.SaveChangesAsync();
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }
        }
        public async Task UpdateDelivery(Guid employeeID){
            try {

            }
        }
    }
}