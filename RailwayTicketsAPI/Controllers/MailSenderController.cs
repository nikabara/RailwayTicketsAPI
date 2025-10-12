using Application.Abstractions;
using Application.DTOs.EmailDTOs;
using Infrastructure.BusinessLogics;
using Infrastructure.ExternalServices.EmailSendingService.Abstractions;
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
            var targetUser = await _userRepository.GetUserByEmail(sendEmailDTO.ToEmail);

            if (targetUser != null)
            {
                SMTPEmailSendingBusinessLogic _smptEmailSendingBusinessLogic = new (
                    _userRepository, 
                    _smptEmailSender, 
                    targetUser.UserId, 
                    sendEmailDTO.Subject, 
                    sendEmailDTO.Message 
                );

                await _smptEmailSendingBusinessLogic.Execute();

                return Ok("Email sent successfully.");
            }
            else
            {
                return BadRequest("User with the specified email was not found.");
            }
        }
        #endregion
    }
}
