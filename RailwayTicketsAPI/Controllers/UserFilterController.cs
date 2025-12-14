using Application.DTOs.UserDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFilterController : ControllerBase
    {
        #region Properties
        private readonly IUserFilterService _userFilterService;
        #endregion

        #region Constructors
        public UserFilterController(IUserFilterService userFilterService)
        {
            _userFilterService = userFilterService;
        }
        #endregion

        #region Methods
        [HttpPost("filter-users")]
        public async Task<ActionResult<ServiceResponse<UserFilterDTO>>> FilterUsers(UserFilterDTO userFilterDTO)
        {
            var response = await _userFilterService.FilterUsers(userFilterDTO);

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
