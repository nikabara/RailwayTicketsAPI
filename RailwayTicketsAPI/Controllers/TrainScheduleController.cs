using Application.DTOs.TrainScheduleDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainScheduleController : ControllerBase
    {
        #region Fields
        private readonly ITrainScheduleService _trainScheduleService;
        #endregion

        #region Constructors
        public TrainScheduleController(ITrainScheduleService trainScheduleService)
        {
            _trainScheduleService = trainScheduleService;
        }
        #endregion

        #region Methods
        [HttpGet("get-train-schedule/{trainScheduleId:int}")]
        public async Task<ActionResult<ServiceResponse<GetTrainScheduleDTO>>> GetTrainSchedule(int trainScheduleId)
        {
            var response = await _trainScheduleService.GetTrainScheduleByID(trainScheduleId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("add-train-schedule")]
        public async Task<ActionResult<ServiceResponse<int>>> AddTrainSchedule(AddTrainScheduleDTO addTrainScheduleDTO)
        {
            var response = await _trainScheduleService.AddTrainSchedule(addTrainScheduleDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("remove-train-schedule/{trainScheduleId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveTrainSchedule(int trainScheduleId)
        {
            var response = await _trainScheduleService.RemoveTrainSchedule(trainScheduleId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("update-train-schedule")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateTrainSchedule(UpdateTrainScheduleDTO updateTrainScheduleDTO)
        {
            var response = await _trainScheduleService.UpdateTrainSchedule(updateTrainScheduleDTO);

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
