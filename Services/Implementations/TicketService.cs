using Backend.Services.Interface;
using Backend.Models.DTOs.Request;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _dbcontext;
        public TicketService (AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task <List<Ticket>?> GetAllTicketIn(DateOnly start, DateOnly end) =>
            await _dbcontext.Ticket
            .AsNoTracking()
            .Where( b => b.StartDate >= start.ToDateTime(TimeOnly.MinValue) &&
                    b.EndDate <= end.ToDateTime(TimeOnly.MaxValue) &&
                    b.DeletedAt == null
                )
            .ToListAsync();

        public async Task <Ticket?> GetTicketByID(Guid ticketID) =>
            await _dbcontext.Ticket
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TicketID == ticketID && t.DeletedAt == null);


        public async Task UpdateTicket(Guid ticketID, TicketUpdateRequest request)
        {
            var ticket = await _dbcontext.Ticket.FindAsync(ticketID); 

            if(ticket == null)
            {
                throw new Exception("Ticket not found");
            }

            try
            {
                if(request.StartDate.HasValue)
                    ticket.StartDate = request.StartDate.Value;
                
                if(request.EndDate.HasValue)
                    ticket.EndDate = request.EndDate.Value;

                if(request.Discount.HasValue)
                    ticket.Discount = request.Discount.Value;

                await _dbcontext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine($"Update ticket Error {ex.Message}");
                throw new Exception($"An error occurred while updating the ticket: {ex.Message}");
            }
        }

        public async Task SoftDeleteTicket(Guid ticketID)
        {
            var ticket = await _dbcontext.Ticket
                .FirstOrDefaultAsync(t => t.TicketID == ticketID &&
                                    t.DeletedAt == null);
            
            if(ticket == null)
            {
                throw new Exception("Ticket not found");
            }
            
            try
            {
                ticket.DeletedAt = DateTime.Now;
                await _dbcontext.SaveChangesAsync();
            }catch(Exception ex)
            {
                Console.WriteLine($"Delete ticket Error {ex.Message}");
                throw new Exception("An error occurred while deleting ticket");
            }
        }
    }
}

