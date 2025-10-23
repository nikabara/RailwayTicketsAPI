using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public SeatRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<int?> AddSeat(Seat seat)
    {
        var result = default(int?);

        await _dbContext.Seats.AddAsync(seat);

        var rowsAffected = await _dbContext.SaveChangesAsync();

        return result = rowsAffected > 0 
            ? seat.SeatId 
            : null;
    }

    public async Task<Seat?> GetSeat(int id)
    {
        var result = new Seat();

        result = await _dbContext.Seats
            .Include(s => s.Vagon)
            .Include(s => s.Tickets)
            .FirstOrDefaultAsync(s => s.SeatId == id);

        return result;
    }

    public async Task<bool> RemoveSeat(int id)
    {
        var result = default(bool);

        var targetSeat = await _dbContext.Seats.FirstOrDefaultAsync(s => s.SeatId == id);

        if (targetSeat == null)
        {
            result = false;
        }
        else
        {
            _dbContext.Seats.Remove(targetSeat);

            var rowsAffected = await _dbContext.SaveChangesAsync();

            result = rowsAffected > 0;
        }

        return result;
    }

    public async Task<bool> UpdateSeat(Seat seat)
    {
        var result = default(bool);

        var targetSeat = await _dbContext.Seats.FirstOrDefaultAsync(s => s.SeatId == seat.SeatId);

        if (targetSeat == null)
        {
            result = false;
        }
        else
        {
            targetSeat.VagonId = seat.VagonId == null
                ? targetSeat.VagonId
                : seat.VagonId;

            targetSeat.SeatNumber = string.IsNullOrWhiteSpace(seat.SeatNumber)
                ? targetSeat.SeatNumber
                : seat.SeatNumber;

            targetSeat.SeatPrice = seat.SeatPrice == null
                ? targetSeat.SeatPrice
                : seat.SeatPrice;

            targetSeat.IsOccupied = seat.IsOccupied == null
                ? targetSeat.IsOccupied 
                : seat.IsOccupied;

            var rowsAffected = await _dbContext.SaveChangesAsync();
            result = rowsAffected > 0;
        }

        return result;
    }
    #endregion
}
