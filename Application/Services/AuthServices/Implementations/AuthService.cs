using Application.Abstractions;
using Application.Common;
using Application.DTOs.AuthDTOs;
using Application.Services.AuthServices.Abstractions;
using Application.Services.ExternalServices.EmailSendingService.Abstractions;
using Domain.Common;
using Domain.Entities;
using Infrastructure.BusinessLogics;
using Microsoft.Extensions.Configuration;

namespace Application.Services.AuthServices.Implementations;

public class AuthService : IAuthService
{
    #region Properties
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly ISMTPEmailSender _smtpEmailSender;
    private readonly IVerificationCodeRepository _verificationCodeRepository;
    #endregion

    #region Constructor
    public AuthService(IUserRepository userRepository, ISMTPEmailSender smtpEmailSender, IConfiguration configuration, IVerificationCodeRepository verificationCodeRepository)
    {
        _userRepository = userRepository;
        _smtpEmailSender = smtpEmailSender;
        _configuration = configuration;
        _verificationCodeRepository = verificationCodeRepository;
    }
    #endregion

    #region Public Methods
    public async Task<ServiceResponse<string>> LogIn(string email, string password)
    {
        var response = new ServiceResponse<string>();

        var targetUser = await _userRepository.GetUserByEmail(email);

        if (targetUser == null)
        {
            response.ErrorMessage = $"User with email: {email} and password: {password} was not found";
            response.IsSuccess = false;
        }
        else if (!Utilities.VerifyPasswordHash(password, targetUser.PasswordHash, targetUser.PasswordSalt))
        {
            response.ErrorMessage = $"Password: {password} was incorrect";
            response.IsSuccess = false;
        }
        else
        {
            response.Data = Utilities.GenerateToken(_configuration, targetUser);
            response.IsSuccess = true;
        }

        return response;
    }

    public async Task<ServiceResponse<int>> RegisterUser(RegisterUserDTO registerUserDTO)
    {
        var response = new ServiceResponse<int>();

        var targetUser = await _userRepository.GetUserByEmail(registerUserDTO.Email);

        if (targetUser == null)
        {
            Utilities.GeneratePasswordHash(registerUserDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var newUser = new User
            {
                Name = registerUserDTO.Name,
                LastName = registerUserDTO.LastName,
                Age = registerUserDTO.Age,
                Email = registerUserDTO.Email,
                UserBalance = 0,
                PhoneNumber = registerUserDTO.PhoneNumber,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                RegistrationDate = DateTime.Now
            };

            var createdUserId = await _userRepository.AddUser(newUser);

            if (createdUserId != null && createdUserId > 0)
            {
                response.IsSuccess = true;
                response.Data = (int)createdUserId;
            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage = "An error occurred while creating the user.";
            }
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"User with email {registerUserDTO.Email} is already registered in system!";
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> SendVerificationCode(int userID)
    {
        var response = new ServiceResponse<bool>();

        var targetUser = await _userRepository.GetUserByID(userID);

        if (targetUser == null)
        {
            response.ErrorMessage = $"User with ID: {userID} was not found";
            response.IsSuccess = false;
        }
        else
        {
            var code = new Random().Next(100000, 999999).ToString();

            var verificationCode = new VerificationCode
            {
                Email = targetUser.Email,
                Code = code,
                ExpirationDate = DateTime.Now.AddMinutes(5),
                IsUsed = false
            };

            await _verificationCodeRepository.AddVerificationCode(verificationCode);

            var emailSendingBL = new SMTPEmailSendingBusinessLogic(_userRepository, _smtpEmailSender, targetUser.UserId, "Verification code", code);

            var emailSendingResult = await emailSendingBL.Execute();

            if (emailSendingResult.IsError)
            {
                response.IsSuccess = false;
                response.Data = false;
                response.ErrorMessage = emailSendingResult.ErrorMessage;
            }
            else
            {
                response.IsSuccess = true;
                response.Data = true;
            }
        }

        return response;
    }

    public Task<ServiceResponse<bool>> VerifyVerificationCode(string email, string code)
    {
        throw new NotImplementedException();
    }
    #endregion
}
