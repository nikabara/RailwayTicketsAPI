using Application.DTOs.TrainScheduleDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainsSchedulesFilterController : ControllerBase
    {
        #region Properties
        private readonly ITrainScheduleFilterService _trainSheduleFilterService;
        #endregion

        #region Constructors
        public TrainsSchedulesFilterController(ITrainScheduleFilterService trainSheduleFilterService)
        {
            _trainSheduleFilterService = trainSheduleFilterService;
        }
        #endregion

        #region Methods
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TrainAndScheduleFilterDTO>>> FilterTrainSchedule(TrainAndScheduleFilterDTO filterOptions)
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
