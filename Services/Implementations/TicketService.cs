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
            .Include(t => t.TicketProduct)
                .ThenInclude(tp => tp.ProductVarient)
            .ToListAsync();

        public async Task <Ticket?> GetTicketByID(Guid ticketID) =>
            await _dbcontext.Ticket
            .AsNoTracking()
            .Include(t => t.TicketProduct)
                .ThenInclude(tp => tp.ProductVarient)
            .FirstOrDefaultAsync(t => t.TicketID == ticketID && t.DeletedAt == null);

        // public async async AddTicket(TicketCreateRequest createRequest)
        // {
        //     using var transaction = await _dbcontext.Database.BeginTransactionAsync();
        //     try
        //     {
        //         var newticket = new Ticket
        //         {
        //             StartDate = createRequest.StartDate,
        //             EndDate = createRequest.EndDate,
        //             Discount = createRequest.Discount
        //             IsActive = true
        //         };

        //         foreach(var product in createRequest)
        //     }
        //     await transaction.CommitAsync();
        // }

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

