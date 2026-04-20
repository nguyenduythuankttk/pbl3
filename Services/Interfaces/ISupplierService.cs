using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;

namespace Backend.Services.Interface
{
    public interface ISupplierService
    {
        Task<List<Supplier>?> GetAllSuppliers();
        Task<Supplier?> GetSupplierByID(int supllierID);
        Task AddSupplier(Supplier supplier);
        Task UpdateSupplier(int supplierID, SupplierUpdateRequest request);
        Task DeleteSupplier(int supplierID);
    }
}
