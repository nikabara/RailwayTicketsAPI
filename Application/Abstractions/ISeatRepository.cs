using Domain.Entities;

namespace Application.Abstractions;

public interface ISeatRepository
{
    public Task<int?> AddSeat(Seat seat);
    public Task<bool> RemoveSeat(int id);
    public Task<Seat?> GetSeat(int id);
    public Task<bool> UpdateSeat(Seat seat);
}
