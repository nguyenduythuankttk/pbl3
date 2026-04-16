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
                .Include(di => di.DeliveryLog.OrderByDescending(l => l.ChangeAt).Take(1))
                .Where(di => di.DeliveryLog.Any() &&
                            di.DeliveryLog.Max(l => l.ChangeAt) >= start &&
                            di.DeliveryLog.Max(l => l.ChangeAt) <= end)
                .ToListAsync();
        public async Task <List<DeliveryInfo>?> GetAllDeliveryByUser(Guid userID) =>
            await _dbcontext.DeliveryInfo
                .Where (d => d.UserID == userID)
                .Include(d => d.User)
                .Include (d => d.DeliveryLog
                .OrderByDescending(l =>l.ChangeAt)
                .Take(1))
                .Include (d => d.Bill)
                .Include (d => d.Address)
                .ToListAsync();
        public async Task AddDeliveryInfo(DeliveryInfoCreateRequest request){
            try {
                var delivery = new DeliveryInfo{
                    BillID = request.BillID,
                    UserID = request.UserID,
                    AddressID = request.AddressID,
                    ShippingFee = request.ShippingFee,
                    Note = request.Note
                };
                _dbcontext.DeliveryInfo.Add(delivery);
                await _dbcontext.SaveChangesAsync();
            } catch (Exception e){
                Console.WriteLine(e.Message);
            }   
        }
        public async Task UpdateDelivery(Guid deliveryID, DeliveryUpdateRequest updateRequest){
            try{
                var delivery = _dbcontext.DeliveryInfo
                                .FirstOrDefault(d =>d.DeliveryID == deliveryID);
                if (delivery != null){
                    var Log = new DeliveryLog {
                        EmployeeID = updateRequest.EmployeeID,
                        Status = updateRequest.Status,
                        ChangeAt = updateRequest.ChangeAt,
                        Note = updateRequest.Note
                    };
                    _dbcontext.DeliveryLog.Add(Log);
                    await _dbcontext.SaveChangesAsync();
                }   

            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}