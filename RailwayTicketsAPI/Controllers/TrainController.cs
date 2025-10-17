using Application.DTOs.TrainDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        #region Properties
        private readonly ITrainService _trainService;
        #endregion

        #region Constructors
        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }
        #endregion

        #region Actions
        [HttpGet("get-train/{trainId:int}")]
        public async Task<ActionResult<ServiceResponse<GetTrainDTO>>> GetTrain(int trainId)
        {
            var response = await _trainService.GetTrainByID(trainId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("add-train")]
        public async Task<ActionResult<ServiceResponse<int>>> AddTrain(AddTrainDTO addTrainDTO)
        {
            var response = await _trainService.AddTrain(addTrainDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("remove-train/{trainId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveTrain(int trainId)
        {
            var response = await _trainService.RemoveTrain(trainId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("update-train")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateTrain(UpdateTrainDTO updateTrainDTO)
        {
            var response = new ServiceResponse<bool>();

            response = await _trainService.UpdateTrain(updateTrainDTO);

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
