using Application.AuthServices.Abstractions;
using Application.DTOs.AuthDTOs;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Properties
        private readonly IAuthService _authService;
        #endregion

        #region Constructors
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Methods
        [HttpPost("log-in")]
        public async Task<ActionResult<ServiceResponse<int>>> LogIn(string email, string password)
        {
            var response = await _authService.LogIn(email, password);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response + " " + response.ErrorMessage);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(RegisterUserDTO registerUserDTO)
        {
            var response = await _authService.RegisterUser(registerUserDTO);
            
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response + " " + response.ErrorMessage);
            }
        }
        #endregion
    }
}
