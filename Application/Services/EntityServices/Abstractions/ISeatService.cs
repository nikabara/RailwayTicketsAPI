using Application.DTOs.SeatDTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Abstractions;

public interface ISeatService
{
    public Task<ServiceResponse<bool>> BookSeat(BookSeatDTO bookSeatDTO);
    public Task<ServiceResponse<List<GetSeatDTO>>> GetSeatsByVagonID(int vagonId);
    public Task<ServiceResponse<bool>> RemoveAllVagonSeats(int vagonId);
    public Task<ServiceResponse<GetSeatDTO>> GetSeatById(int seatId);
    public Task<ServiceResponse<bool>> RemoveSeat(int seatId);
}
