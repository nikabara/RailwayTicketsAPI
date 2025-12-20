using Application.DTOs.VagonDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VagonFilterController : ControllerBase
    {
        #region Properties
        private readonly IVagonFilterService _vagonFilterService;
        #endregion

        #region Constructors
        public VagonFilterController(IVagonFilterService vagonFilterService)
        {
            _vagonFilterService = vagonFilterService;
        }
        #endregion

        #region Methods
        [HttpPost("filter-vagons")]
        public async Task<ActionResult<ServiceResponse<List<GetVagonDTO>>>> GetFilteredVagons(VagonFilterDTO filterOptions)
        {
            var response = await _vagonFilterService.GetFilteredVagons(filterOptions);

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
