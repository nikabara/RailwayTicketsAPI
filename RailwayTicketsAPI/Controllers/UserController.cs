using Application.DTOs.UserDTO;
using Application.Services.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Properties
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Endpoints
        [HttpPost("add-user")]
        public async Task<ActionResult<ServiceResponse<int>>> AddUser(AddUserDTO addUserDTO)
        {
            var response = await _userService.AddUser(addUserDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet("get-user/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetUser(int userId)
        {
            var response = await _userService.GetUserByID(userId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("delete-user/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser(int userId)
        {
            var response = await _userService.RemoveUser(userId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut("update-user")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser(UpdateUserDTO updateUserDTO)
        {
            var response = await _userService.UpdateUser(updateUserDTO);
         
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
