using Application.DTOs.SeatDTOs;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface ISeatService
{
    public Task<ServiceResponse<bool>> BookSeat(BookSeatDTO bookSeatDTO);
}
