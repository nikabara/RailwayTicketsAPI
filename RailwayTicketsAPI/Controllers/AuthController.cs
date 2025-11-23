using Application.DTOs.AuthDTOs;
using Application.Services.AuthServices.Abstractions;
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

            //var cookiesOptions = new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = false,
            //    SameSite = SameSiteMode.Unspecified,
            //    Expires = DateTime.UtcNow.AddDays(1)
            //};

            //Response.Cookies.Append("jwt_access_token", response.Data, cookiesOptions);

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

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<bool>>> ResetPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        [HttpPost("send-verification-code/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<int?>>> SendVerificationCode(int userId)
        {
            var response = await _authService.SendVerificationCode(userId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response + " " + response.ErrorMessage);
            }
        }

        [HttpPost("verify-verification-code")]
        public async Task<ActionResult<ServiceResponse<bool>>> VerifyVerificationCode(string email, string code)
        {
            var response = await _authService.VerifyVerificationCode(email, code);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response + " " + response.ErrorMessage);
            }
        }

        [HttpPost("is-user-admin/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> IsUserAdmin(int userId)
        {
            var response = await _authService.VerifyAdminUser(userId);

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
