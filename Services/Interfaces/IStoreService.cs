using Backend.Models.DTOs;

namespace Backend.Services.Interface
{
    public interface IStoreService
    {
        Task<List<Store?>> GetAllStore();
        Task<Store?> GetStoreByID (int storeID);
        Task<List<Store?>> GetStoreByAdress(Guid addressID);
        Task AddStore();
        Task UpdateStore (int StoreID, StoreUpdateRequest request);
        Task DeleteStore(int StoreID);
    }
}