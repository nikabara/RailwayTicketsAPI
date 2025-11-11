using Domain.Entities;

namespace Application.Abstractions;

public interface ITicketRepository
{
    public Task<int?> AddTicket(Ticket ticket);
    public Task<bool> RemoveTicket(int id);
    public Task<bool> UpdateTicket(Ticket ticket);
    public Task<Ticket> GetTicket(int id);
}
