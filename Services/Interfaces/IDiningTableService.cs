using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
namespace Backend.Services.Interface{
    public interface IDiningTableService{
        Task<TableListResponse?> GetAllTablesAtStore(int storeID);
        Task<DiningTableResponse?> GetTableByID(int tableID);
        Task AddTable (DiningTable newTable);
        Task UpdateTable (int tableID, int capacity);
        Task SoftDeleteTable (int tableID);
    }
}