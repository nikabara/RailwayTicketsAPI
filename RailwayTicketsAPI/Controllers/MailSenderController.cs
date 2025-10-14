using Application.Abstractions;
using Application.DTOs.EmailDTOs;
using Application.ExternalServices.EmailSendingService.Abstractions;
using Infrastructure.BusinessLogics;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailSenderController : ControllerBase
    {
        #region Properties
        private readonly IUserRepository _userRepository;
        private readonly ISMTPEmailSender _smptEmailSender;
        #endregion

        #region Constructors
        public MailSenderController(IUserRepository userRepository, ISMTPEmailSender smptEmailSender)
        {
            _userRepository = userRepository;
            _smptEmailSender = smptEmailSender;
        }
        #endregion

        #region Methods
        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(SendEmailDTO sendEmailDTO)
        {
            SMTPEmailSendingBusinessLogic _smptEmailSendingBusinessLogic = new (
                _userRepository, 
                _smptEmailSender, 
                sendEmailDTO.UserId, 
                sendEmailDTO.Subject, 
                sendEmailDTO.Message 
            );

            var result = await _smptEmailSendingBusinessLogic.Execute();

            if (result.IsError)
            {
                return BadRequest(result.ErrorMessage);
            }
            else
            {
                return Ok("Email was sent successfully");
            }
        }
        #endregion
    }
}
