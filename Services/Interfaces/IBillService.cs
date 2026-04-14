using Backend.Models;
using Backend.Models.DTOs;
namespace Backend.Services.Interface{
    public interface IBillService{
        Task<List<Bill>> GetAllBillIn(DateOnly dayStart,DateOnly dayEnd);
        Task<List<Bill>?> GetUserBill(Guid userID);
        Task<List<Bill>?> GetUserBillSuccess(Guid userID);
        Task<List<Bill>?> GetUserBillPending(Guid userID);
        Task<List<Bill>?> GetUserBillFail(Guid userID);
        Task AddBill(Bill bill);
        Task <Bill?> GetBillByID(Guid billID);
        Task RemoveBill(Guid billID);
        Task DeleteBill(Guid b);
    }
}