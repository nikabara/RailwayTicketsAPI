using Application.DTOs.TrainScheduleDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleFilterController : ControllerBase
    {
        #region Properties
        private readonly ITrainScheduleFilterService _trainSheduleFilterService;
        #endregion

        #region Constructors
        public ScheduleFilterController(ITrainScheduleFilterService trainSheduleFilterService)
        {
            _trainSheduleFilterService = trainSheduleFilterService;
        }
        #endregion

        #region Methods
        [HttpPost("filter-schedules")]
        public async Task<ActionResult<ServiceResponse<ScheduleFilterDTO>>> FilterSchedule(ScheduleFilterDTO filterOptions)
        {
            var response = await _trainSheduleFilterService.Filter(filterOptions);

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
