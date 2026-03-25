using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Services.Interface;
using Microsoft.EntityFrameWorkCore;
namespace BackEnd.Services.Implementations{
    public class AddressService : IAddressService{
        private readonly AppDbContext _dbContext;

        public AddressService(AppDbContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<List<Address>> GetStoreAddress() =>
            await _dbContext.Address
            .Include(a => a.Store)
            .Where(a => a.Store != null)
            .ToListAsync();
        public async Task<List<Address>> GetSupplierAddress() => 
            await _dbContext.Address
            .Include(a => a.Supplier)
            .Where (a => a.Supplier != null)
            .ToListAsync();
        public async Task<List<Address>> GetUserAddress(User user)=>
            await _dbContext.UserAddress
            .Where (ua => ua.User.UserID == user.UserID)
            .Select (ua => ua.Address)
            .ToListAsync();
        public async Task AddAddress(Address address){
            try{
                _dbContext.Address.Add(address); 
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex){
                throw;
            }    
        }
        public async Task DeleteUserAddress(Address address,User user){
            try{
                var userAddress = await _dbContext.UserAddress
                        .FirstOrDefaultAsync(ua => ua.User.UserID ==user.UserID &&  ua.Address.AddressID == address.AddressID);
                if (userAddress != null){
                    _dbContext.UserAddress.Remove(userAddress);
                    await _dbContext.SaveChangesAsync();
                }
            }catch
            {
                throw;
            }
        }
        
        
    }
} 
// bất đồng bộ và đồng bộ
