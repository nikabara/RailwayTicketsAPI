using Application.DTOs.VagonDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    //[Authorize(Roles = "SuperAdmin, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class VagonController : ControllerBase
    {
        #region Properties
        private readonly IVagonService _vagonService;
        #endregion

        #region Constructors
        public VagonController(IVagonService vagonService)
        {
            _vagonService = vagonService;
        }
        #endregion

        #region Methods
        [HttpPost("add-vagon")]
        public async Task<ActionResult<ServiceResponse<int?>>> AddVagon(AddVagonDTO addVagonDTO)
        {
            var response = new ServiceResponse<int?>();

            response = await _vagonService.AddVagon(addVagonDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-vagon/{VagonId:int}")]
        public async Task<ActionResult<ServiceResponse<GetVagonDTO>>> GetVagon(int VagonId)
        {
            var response = new ServiceResponse<GetVagonDTO?>();

            response = await _vagonService.GetVagonByID(VagonId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("delete-vagon/{VagonId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveVagon(int VagonId)
        {
            var response = new ServiceResponse<bool>();

            response = await _vagonService.RemoveVagon(VagonId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("update-vagon")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateVagon(UpdateVagonDTO updateVagonDTO)
        {
            var response = new ServiceResponse<bool>();

            response = await _vagonService.UpdateVagon(updateVagonDTO);

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
