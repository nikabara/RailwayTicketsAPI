using Application.Abstractions;
using Application.Services.ExternalServices.EmailSendingService.Abstractions;
using Domain.Entities;

namespace Infrastructure.BusinessLogics;

public class SMTPEmailSendingBusinessLogic
{
    #region Properties
    private User _user;
    private readonly int _userID;
    private readonly string _emailSubject;
    private readonly string _emailMessage;

    private readonly IUserRepository _userRepository;
    private readonly ISMTPEmailSender _smptEmailSender;

    SMTPEmailSendingResult _result = new();
    #endregion

    #region Constructors
    public SMTPEmailSendingBusinessLogic(IUserRepository userRepository, ISMTPEmailSender smptEmailSender, int userID, string emailSubject, string emailMessage)
    {
        _userRepository = userRepository;
        _smptEmailSender = smptEmailSender;
        _userID = userID;
        _emailMessage = emailMessage;
        _emailSubject = emailSubject;
    }
    #endregion

    #region Methods
    public async Task<SMTPEmailSendingResult> Execute()
    {
        await InitBusinessLogicProperties();

        if (!_result.IsError)
        {
            await SendEmail();
        }

        return _result;
    }

    private async Task InitBusinessLogicProperties()
    {
        var user = await _userRepository.GetUserByID(_userID);

        if (user == null)
        {
            _result.IsError = true;
            _result.ErrorMessage = $"User with ID {_userID} was not found";
        }
        else
        {
            _result.IsError = false;

            _user = user;
        }
    }

    async Task SendEmail()
    {
        await _smptEmailSender.SendEmail(_user.Email, _emailSubject, _emailMessage, "template_VerificationCodeEmail#1");
    }
    #endregion

    #region Nested classes
    public class SMTPEmailSendingResult
    {
        public bool IsError { get; set; } = false;
        public string ErrorMessage { get; set; } = string.Empty;
    }
    #endregion
}
