using Backend.Models;
using Backend.Models.DTOs.Request;
using Backend.Models.DTOs.Reponse;

namespace Backend.Services.Interface{
    public interface ITicketService {
        Task<List<Ticket>?> GetTicketByUser (Guid user);
        Task<Ticket> GetTicketByID (int ticketID);
        Task AddTicket()
    }
}