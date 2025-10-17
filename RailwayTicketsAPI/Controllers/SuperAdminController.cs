using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        #region Properties
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public SuperAdminController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Methods
        [HttpPut("super-admin/register-admin/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> MakeAdmin(int userId)
        {
            var response = await _userService.MakeUserAdmin(userId);

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
