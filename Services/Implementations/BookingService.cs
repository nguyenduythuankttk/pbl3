using Backend.Data;
using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _dbcontext;
        
        public BookingService(AppDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public async Task<List<Booking>?> GetAllBookingIn() => 
            await _dbcontext.Booking
            .Where(b => b.ScheduledTime >= DateTime.Now.Add(TimeSpan.FromMinutes(-14)))
            .Include(b => b.User)
            .Include(b => b.Table)
            .ToListAsync();    

        public async Task<List<Booking>?> GetBookingByUser() =>
            await _dbcontext.Booking
            .ToListAsync();  
    }
}