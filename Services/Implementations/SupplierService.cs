using System.Security.Cryptography.X509Certificates;
using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Backend.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _dbcontext;
        public SupplierService(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<List<Supplier>?> GetAllSuppliers() =>
            await _dbcontext.Supplier
                .Where(s => s.DeletedAt == null)
                .AsNoTracking()
                .Include(s => s.Address)
                    .ThenInclude(sa => sa.Store)
            .ToListAsync();

        public async Task <Supplier?> GetSupplierByID(int supplierID) =>
            await _dbcontext.Supplier
                .AsNoTracking()
                    .Include(s => s.Address)
                        .ThenInclude(sa => sa.Store)
            .FirstOrDefaultAsync(s => s.SupplierID == supplierID && s.DeletedAt == null);


        public async Task AddSupplier(Supplier supplier)
        {
            try
            {
                _dbcontext.Supplier.Add(supplier);
                await _dbcontext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine($"Add Supplier Error: {ex.Message}");
            }
        }

        public async Task UpdateSupplier(int supplierID, SupplierUpdateRequest request)
        {
            var supplier = await _dbcontext.Supplier.FindAsync(supplierID);

            if(supplier == null)
            {
                throw new Exception("Supplier not found");
            }

            try
            {
                if(request.SupplierName != null)
                    supplier.SupplierName = request.SupplierName;
                
                if(request.Phone != null)
                    supplier.Phone = request.Phone;

                if(request.Email != null)
                    supplier.Email = request.Email;

                if(request.TaxCode != null)
                    supplier.TaxCode = request.TaxCode;

                await _dbcontext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine($"Update supplier error: {ex.Message}");
                throw new Exception($"An error occurred while updating the user: {ex.Message}");
            }
        }

        public async Task DeleteSupplier(int supplierID)
        {
            var supplier = await _dbcontext.Supplier
                .FirstOrDefaultAsync(s => s.SupplierID == supplierID &&
                                    s.DeletedAt == null);
            
            if(supplier == null)
            {
                throw new Exception("Supplier not found");
            }

            try
            {
                supplier.DeletedAt = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine($"Delete supplier error {ex.Message}");
                throw new Exception($"An errorr occurred while deleting supplier: {ex.Message} ");
            }
        }
    }
}