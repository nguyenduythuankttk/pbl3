using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;

namespace Backend.Services.Interface
{
    public interface IStoreService
    {
        Task<List<Store>?> GetAllStore();
        Task<Store?> GetStoreByID (int storeID);
        Task<Store?> GetStoreByAdress(Guid addressID);
        Task AddStore(Store store);
        Task UpdateStore (int StoreID, StoreUpdateRequest request);
        Task SoftDeleteStore(int StoreID);
    }
}