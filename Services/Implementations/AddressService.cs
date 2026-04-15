
using Backend.Data;
using Backend.Models;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services.Implementations{
    public class AddressService : IAddressService{
        private readonly AppDbContext _dbContext;

        public AddressService(AppDbContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<Address?> GetAddressByID (Guid addressID) => 
        await _dbContext.Address
                .Where(a => a.AddressID == addressID)
                .FirstOrDefaultAsync();
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
            .Where (ua => ua.UserID == user.UserID)
            .Select (ua => ua.Address)
            .ToListAsync();
        public async Task AddUserAddress(Address address, Guid userID){
            try{
                _dbContext.Address.Add(address); 
                await _dbContext.SaveChangesAsync();
                bool hadAddress = _dbContext.UserAddress
                                    .AnyAsync(ua => ua.UserID == userID);
                var newUA = new UserAddress{
                    UserID = userID,
                    AddressID = address.AddressID,
                    IsDefault = !hadAddress
                };
                _dbContext.UserAddress.Add(newUA);
                await _dbContext.SaveChangesAsync();
            } catch (Exception ex){
                Console.WriteLine(ex.Message);
            }    
        }
            public async Task AddAddress(AddressCreateRequest request){
            try{
                var address = new Address{
                    HouseNumber = request.HouseNumber,
                    
                }
            }
        }
        public async Task DeleteUserAddress(Guid address,Guid user){
            try{
                var userAddress = await _dbContext.UserAddress
                        .FirstOrDefaultAsync(ua => ua.User.UserID ==user &&  ua.Address.AddressID == address);
                if (userAddress != null){
                    _dbContext.UserAddress.Remove(userAddress);
                    await _dbContext.SaveChangesAsync();
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task SetDefault(Guid address, Guid user){
            try{
                var newDefault = await _dbContext.UserAddress
                                    .FirstOrDefaultAsync(ua => ua.User.UserID == user && ua.Address.AddressID == address);
                var oldDefault = await _dbContext.UserAddress
                                    .FirstOrDefaultAsync(ua => ua.User.UserID == user && ua.IsDefault == true);
                if (oldDefault != null){
                    oldDefault.IsDefault = false;
                    _dbContext.UserAddress.Update(oldDefault);
                    await _dbContext.SaveChangesAsync();
                }
                if (newDefault != null){
                    newDefault.IsDefault = true;
                    _dbContext.UserAddress.Update(newDefault);
                    await _dbContext.SaveChangesAsync();
                }
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
            }
        }
        
    }
} 


