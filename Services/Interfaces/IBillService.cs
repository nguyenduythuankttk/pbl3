using Backend.Models;
using Backend.Models.DTOs;
namespace Backend.Services.Interface{
    public interface IBillService{
        Task<List<Bill>> GetAllBillIn(DateOnly dayStart,DateOnly dayEnd);
<<<<<<< HEAD
        Task<List<Bill>?> GetUserBill(Guid userID);
        Task<List<Bill>?> GetUserBillSuccess(Guid userID);
        Task<List<Bill>?> GetUserBillPending(Guid userID);
        Task<List<Bill>?> GetUserBillFail(Guid userID);
        Task AddBill(Bill bill);
        Task <Bill?> GetBillByID(Guid billID);
        Task RemoveBill(Guid billID);
        Task DeleteBill(Guid b);
=======
        Task<List<Bill>> GetByBillID();
        Task<List<Bill>> GetUserBill(Guid userID);
        Task<List<Bill>>GetUserBillSuccess(Guid userID);
        Task<List<Bill>>GetUserBillPending(Guid userID);
        Task<List<Bill>>GetUserBillFail(Guid userID);
        Task AddBill();
        Task RemoveBill(string? note);
        Task <Bill?> FindBill();
        Task RemoveBill();
        //Task DeleteBill(BillDeleteRequest b);
>>>>>>> 3f996c8133d0c3e2b659425e1aa1cdd644fb15df
    }
}