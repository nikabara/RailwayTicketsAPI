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
        #endregion
    }
}
