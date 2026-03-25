using BackEnd.Models;
using BackEnd.Models.DTOs;
namespace BackEnd.Services.Interface{
    public interface IAddressService{
        Task<List<Address>> GetAllAddresses();
        Task<Address?> GetAddressByID (int addressID);
        Task AddAddress(Address address);
        Task UpdateAddress(Guid addressID, AddressRequest request);
        Task DeleteAddress(Guid addressID);
    }
}