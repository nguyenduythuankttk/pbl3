using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Services.Interface
{
    public interface IReceiptService
    {
        Task<List<ReceiptItem>> GetAllReceipt();
        Task<ReceiptItem?> GetReceiptbyID(Guid ReceiptID);
        Task AddReceipt(ReceiptItem receiptItem);
        Task UpdateReceipt(Guid ReceiptID, ReceiptUpdateRequest request);
        Task DeleteReceipt(Guid ReceiptID);
    }
}