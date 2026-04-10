using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;

namespace Backend.Services.Interface{
    public interface IAddressService{
        Task<List<Address>> GetAllAddresses();
        Task<Address?> GetAddressByID (int addressID);
        Task AddAddress(Address address);
        Task UpdateAddress(Guid addressID, AddressRequest request);
        Task DeleteAddress(Guid addressID);
        Task SetDefault(Address address, User user);
    }
}