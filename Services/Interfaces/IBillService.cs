using Backend.Models;
using Backend.Models.DTOs;
namespace Backend.Services.Interface{
    public interface IBillService{
        Task <List<Bill>?> GetAllBillIn(DateOnly start, DateOnly end);
        Task <List<Bill>?> GetUserBill(Guid userID);
        Task <List<Bill>?> GetUnPaidBill();
        Task <List<Bill>?> GetPaidBill();
        Task <List<Bill>?> GetDeletedBill();
        Task <List<Bill>?> GetCashBill();
        Task <List<Bill>?> GetCardBill();
        Task <Bill?> GetBillByID(Guid billID);
        Task AddBill(Bill bill);
        Task SetPaid(Guid billID);
        Task DeleteBill(Guid BillID);
    }
}