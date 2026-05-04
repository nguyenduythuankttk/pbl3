using Backend.Data;
using Backend.Models;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations {
    public class EmployeeService : IEmployeeService {
        private readonly AppDbContext _dbContext;

        public EmployeeService(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<Employee?> GetEmployeeByID(Guid id) {
            return await _dbContext.Employee.FirstOrDefaultAsync(e => e.UserID == id);
        }

        public async Task<List<Employee>?> GetAllEmployee() {
            return await _dbContext.Employee.Where(e => e.DeleteAt == null).ToListAsync();
        }

        public async Task AddEmployee(Employee newEmployee) {
            _dbContext.Employee.Add(newEmployee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SoftDeleteEmployee(Guid id) {
            var employee = await _dbContext.Employee.FirstOrDefaultAsync(e => e.UserID == id);
            if (employee == null) throw new Exception("Không tìm thấy nhân viên");
            employee.DeleteAt = DateTime.UtcNow;
            _dbContext.Employee.Update(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
