using Application.Abstractions;
using Application.DTOs.TicketDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;

namespace Application.Services.EntityServices.Implementations;

public class TicketService : ITicketService
{
    #region Properties
    private readonly ITicketRepository _ticketService;
    #endregion

    #region Constructors
    public TicketService(ITicketRepository ticketService)
    {
        _ticketService = ticketService;
    }
    #endregion

    #region Methods
    public async Task<ServiceResponse<List<GetTicketDTO>>> GetAllUserTickets(int userId)
    {
        var response = new ServiceResponse<List<GetTicketDTO>>();

        var userTickets = await _ticketService.GetAllUserTickets(userId);

        if (userTickets == null)
        {
            response.ErrorMessage = "User has no active tickets";
            response.IsSuccess = false;
        }
        else
        {
            var mappedTickets = userTickets.Select(t => new GetTicketDTO
            {
                TicketId = t.TicketId,
                UserId = t.UserId,
                SeatId = t.SeatId,
                DateOfBooking = t.DateOfBooking,
                TicketPrice = t.TicketPrice,
                TicketPaymentStatusId = t.TicketPaymentStatusId,
                TicketNumber = t.TicketNumber
            }).ToList();

            response.IsSuccess = true;
            response.Data = mappedTickets;
        }

        return response;
    }

    public async Task<ServiceResponse<GetTicketDTO>> GetTicketByTicketNumber(string ticketNumber)
    {
        var response = new ServiceResponse<GetTicketDTO>();

        var targetTicket = await _ticketService.GetTicketWithTicketNumber(ticketNumber);

        if (targetTicket == null)
        {
            response.ErrorMessage = "Ticket not found";
            response.IsSuccess = false;
        }
        else
        {
            var mappedTicket = new GetTicketDTO
            {
                TicketId = targetTicket.TicketId,
                UserId = targetTicket.UserId,
                SeatId = targetTicket.SeatId,
                DateOfBooking = targetTicket.DateOfBooking,
                TicketPrice = targetTicket.TicketPrice,
                TicketPaymentStatusId = targetTicket.TicketPaymentStatusId,
                TicketNumber = targetTicket.TicketNumber
            };

            response.IsSuccess = true;
            response.Data = mappedTicket;
        }

        return response;
    }

    #endregion
}
