using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
namespace Backend.Services.Interface{
    public class IDiningTableService{
        Task <List<DiningTable>?> GetAllTables();
        Task <DiningTable?> GetTableByID();
        Task AddTable (TableCreateRequest table, int tableID);
        
    }
}