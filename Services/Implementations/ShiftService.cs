using Backend.Models;
using Backend.Models.DTOs.Reponse;
using Backend.Models.DTOs.Request;
using Backend.Data;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;
namespace Backend.Services.Implementations{
    public class ShiftService : IShiftService{
        private readonly AppDbContext _dbContext;
        public ShiftService (AppDbContext dbContext){
            _dbContext = dbContext;
        }
        public async Task<List<Shift>?> GetAllShiftIn(DateOnly date) =>
            await _dbContext.Shift.Where (s => s.TimeIn >= date.ToDateTime(TimeOnly.MinValue) &&  s.TimeIn <= date.ToDateTime(TimeOnly.MinValue))
            .Include(s => s.Employee)
            .ToListAsync();
        public async Task<Shift?> GetShiftByID (Guid ID) =>
            await _dbContext.Shift.FirstOrDefaultAsync(s => s.ShiftID == ID)
                    .Include(s => s.Employee);
        public async Task AddShift (ShiftCreateRequest request) {
            try {
                if (request.TimeIn > request.TimeOut){
                    DateTime x = request.TimeIn;
                    request.TimeIn = request.TimeOut;
                    request.TimeOut = x;
                }
                var newShift = new Shift {
                    TimeIn = request.TimeIn,
                    TimeOut = request.TimeOut  
                };
                _dbContext.Shift.Add(newShift);
                _dbContext.SaveChangesAsync();
            } catch (Exception e){
                Console.WriteLine (e.Message);
            }
        }
        public async Task UpdateShift (ShiftUpdateRequest request, Shift shiftID){
            try{
                var shift = await _dbContext.Shift.FirstOrDefaultAsync(s => s.ShiftID == shiftID);
                if (shift != null){
                    shift.CheckIn = request.;
                    shift.CheckOut = request.CheckOut;
                    _dbContext.Shift.Update(shift);
                    await _dbContext.SaveChangesAsync();
                }
            } catch (Exception e){
                Console.WriteLine (e.Message);
            }
        }
        public async Task DeleteShift (Guid ID){
            try{
                var shift = await _dbContext.Shift.FirstOrDefaultAsync(s => s.ShiftID == shiftID);
                if (shift != null){
                    _dbContext.Shift.Remove(shift);
                    await _dbContext.SaveChangesAsync();
                } 
            }catch (Exception e){
                Console.WriteLine(e.Message);        
            }
        }
            
    }
}