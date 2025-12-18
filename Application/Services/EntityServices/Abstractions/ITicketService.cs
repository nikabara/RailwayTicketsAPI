using Application.DTOs.TicketDTOs;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface ITicketService
{
    public Task<ServiceResponse<GetTicketDTO>> GetTicketByTicketNumber(string ticketNumber);
}
