using Backend.Models;
<<<<<<< HEAD:Services/Interfaces/IAddressSevices.cs
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;
=======
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;

>>>>>>> 3f996c8133d0c3e2b659425e1aa1cdd644fb15df:Services/Interfaces/IAddressSevice.cs
namespace Backend.Services.Interface{
    public interface IAddressService{
        Task<Address?> GetAddressByID (Guid addressID);
        Task<List<Address>?> GetStoreAddress();
        Task<List<Address>?> GetSupplierAddress();
        Task<List<Address>> GetUserAddress(User user);
        Task AddAddress(Address address);
        Task DeleteUserAddress(Guid address,Guid user);
        Task SetDefault(Guid address, Guid user);
    }
}