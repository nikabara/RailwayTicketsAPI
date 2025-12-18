using Application.DTOs.TrainDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainFilterController : ControllerBase
    {
        #region Properties
        private readonly ITrainFilterService _trainFilterService;
        #endregion

        #region Constructor
        public TrainFilterController(ITrainFilterService trainFilterService)
        {
            _trainFilterService = trainFilterService;
        }
        #endregion

        #region Methods
        [HttpPost("filter-trains")]
        public async Task<ActionResult<ServiceResponse<List<Train>>>> FilterTrains(TrainFilterDTO filterOptions)
        {
            var response = await _trainFilterService.GetFilteredTrains(filterOptions);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
        #endregion
    }
}
