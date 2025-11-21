using Application.DTOs.SeatDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        #region Properties
        private readonly ISeatService _seatService;
        #endregion

        #region Constructors
        public SeatController(ISeatService seatService)
        {
            _seatService = seatService;
        }
        #endregion

        #region Methods
        [HttpPost("book-seat")]
        public async Task<ActionResult<ServiceResponse<bool>>> BookSeat(BookSeatDTO bookSeatDTO)
        {
            var response = await _seatService.BookSeat(bookSeatDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-all-vagon-seats/{vagonId:int}")]
        public async Task<ActionResult<ServiceResponse<List<GetSeatDTO>>>> GetVagonSeats(int vagonId)
        {
            var response = await _seatService.GetSeatsByVagonID(vagonId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("delet-all-vagon-seats/{vagonId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveAllVAgonSeats(int vagonId)
        {
            var response = await _seatService.RemoveAllVagonSeats(vagonId);

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
