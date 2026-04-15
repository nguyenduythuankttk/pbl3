using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;

namespace Backend.Services.Interface
{
    public interface IReceiptService
    {
        Task<List<Receipt>> GetAllReceipt(DateOnly start, DateOnly end);
        Task<Receipt?> GetReceiptbyID(Guid goodsReceiptID);
        Task<List<Receipt?>> GetReceiptbyPO(Guid pOID);
        Task <List<Receipt?>> GetReceiptbyStore(int storeID);
        Task <List<Receipt?>> GetReceiptbyEmployee(Guid receiptID);
        Task AddReceipt(ReceiptItem receiptItem);
        Task UpdateReceipt(Guid receiptID, ReceiptUpdateRequest request);
        Task DeleteReceipt(Guid receiptID);
    }
}