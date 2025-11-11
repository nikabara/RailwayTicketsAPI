using Application.DTOs.UserCreditCardDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCreditCardController : ControllerBase
    {
        #region Properties
        private readonly IUserCreditCardService _userCreditCardService;
        #endregion

        #region Constructors
        public UserCreditCardController(IUserCreditCardService userCreditCardService)
        {
            _userCreditCardService = userCreditCardService;
        }
        #endregion

        #region Methods
        [HttpGet("get-user-credit-card-sensitive-data/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<GetUserCreditCardsSensitiveDTO>>> GetSensitiveCreditCardInfo(int userId)
        {
            var response = await _userCreditCardService.GetUserCardsSensitive(userId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-user-credit-card-non-sensitive-data/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<GetUserCreditCardsSensitiveDTO>>> GetNonSensitiveCreditCardInfo(int userId)
        {
            var response = await _userCreditCardService.GetUserCardsNonSensitive(userId);

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
