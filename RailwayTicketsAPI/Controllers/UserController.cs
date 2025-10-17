using Application.DTOs.UserDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [HttpPost("admin/add-user")]
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

        [HttpGet("admin/get-user/{userId:int}")]
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

        [HttpDelete("admin/delete-user/{userId:int}")]
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

        [HttpPut("admin/update-user")]
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
