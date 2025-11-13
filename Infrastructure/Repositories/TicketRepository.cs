using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public TicketRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<int?> AddTicket(Ticket ticket)
    {
        var result = default(int);

        ticket.TicketNumber = GenerateTicketNumber();

        await _dbContext.Tickets.AddAsync(ticket);

        var rowsAffected = await _dbContext.SaveChangesAsync();

        if (rowsAffected > 0)
        {
            result = ticket.TicketId;
        }

        return result;
    }

    public async Task<Ticket> GetTicket(int id)
    {
        var result = new Ticket();
        
        var ticket = await _dbContext.Tickets
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.TicketId == id);

        if (ticket != null) result = ticket;

        return result;
    }

    public async Task<Ticket?> GetTicketWithSeatAndUserId(int seatId, int userId)
    {
        Ticket? result = null;

        var ticket = await _dbContext.Tickets
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.SeatId == seatId && t.UserId == userId);
        
        if (ticket != null) result = ticket;
        
        return result;
    }

    public async Task<Ticket> GetTicketWithTicketNumber(string ticketNumber)
    {
        var result = new Ticket();

        var ticket = await _dbContext.Tickets
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.TicketNumber == ticketNumber);

        if (ticket != null) result = ticket;

        return result;
    }

    public async Task<bool> RemoveTicket(int id)
    {
        var result = default(bool);

        var targetTicket = await _dbContext.Tickets.FirstOrDefaultAsync(t => t.TicketId == id);

        if (targetTicket == null)
        {
            result = false;
        }
        else
        {
            _dbContext.Tickets.Remove(targetTicket);

            var rowsAffected = await _dbContext.SaveChangesAsync();

            result = rowsAffected > 0;
        }

        return result;
    }

    public async Task<bool> UpdateTicket(Ticket ticket)
    {
        var result = default(bool);

        if (ticket == null)
        {
            result = false;
        }
        else
        {
            var targetTicket = _dbContext.Tickets.FirstOrDefault(t => t.TicketId == ticket.TicketId);

            if (targetTicket != null)
            {
                targetTicket.SeatId = ticket.SeatId ?? targetTicket.SeatId;

                targetTicket.UserId = ticket.UserId ?? targetTicket.UserId;

                targetTicket.TicketPrice = ticket.TicketPrice ?? targetTicket.TicketPrice;

                targetTicket.TicketPaymentStatusId = ticket.TicketPaymentStatusId ?? targetTicket.TicketPaymentStatusId;

                ticket.TicketNumber = targetTicket.TicketNumber; // non-updatable

                _dbContext.Tickets.Update(targetTicket);
                var rowsAffected = await _dbContext.SaveChangesAsync();
                result = rowsAffected > 0;
            }
        }

        return result;
    }


    private string GenerateTicketNumber() => Guid.NewGuid().ToString()[..36].ToUpper();
    #endregion
}
