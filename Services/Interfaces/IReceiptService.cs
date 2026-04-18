using Backend.Models;
using Backend.Models.DTOs;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;

namespace Backend.Services.Interface
{
    public interface IReceiptService
    {
        Task<List<Receipt>?> GetAllReceiptIn(DateOnly start, DateOnly end);
        Task<Receipt?> GetReceiptByID(Guid goodsReceiptID);
        Task<List<Receipt>?> GetReceiptByPO(Guid pOID);
        Task <List<Receipt>?> GetReceiptByStore(int storeID);
        Task <List<Receipt>?> GetReceiptByEmployee(Guid employeeID);
        Task <List<Receipt>?> GetReceiptBySupplier(Guid supplierID);
        Task AddReceipt(Receipt receipt);
        // Task UpdateReceipt(Guid receiptID, Set request);
        // Task DeleteReceipt(Guid receiptID);
    }
}