using Application.DTOs.TicketDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        #region Properties
        private readonly ITicketService _ticketService;
        #endregion

        #region Constructors
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        #endregion

        #region Methods
        [HttpGet("get-ticket/{ticketNumber:guid}")]
        public async Task<ActionResult<ServiceResponse<GetTicketDTO>>> GetTicketByTicketNumber(string ticketNumber)
        {
            var response = await _ticketService.GetTicketByTicketNumber(ticketNumber);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-all-user-tickets/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<List<GetTicketDTO>>>> GetAllUserTickets(int userId)
        {
            var response = await _ticketService.GetAllUserTickets(userId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("cancel-ticket/{ticketId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> CancelTicket(int ticketId)
        {
            var response = await _ticketService.CancelTicket(ticketId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion
    }
}
