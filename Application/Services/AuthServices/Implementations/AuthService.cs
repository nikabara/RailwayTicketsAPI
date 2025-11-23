using Application.Abstractions;
using Application.Common;
using Application.DTOs.AuthDTOs;
using Application.DTOs.UserDTO;
using Application.Services.AuthServices.Abstractions;
using Application.Services.ExternalServices.EmailSendingService.Abstractions;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
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

            var emailSendingBL = new SMTPEmailSendingBusinessLogic(_userRepository, _smtpEmailSender, targetUser.UserId, "AUTHORIZATION", "Log in was successful");

            var emailSendingResult = await emailSendingBL.Execute();
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

                var emailSendingBL = new SMTPEmailSendingBusinessLogic(_userRepository, _smtpEmailSender, (int)createdUserId, "AUTHORIZATION", "Registration was successful please log in");

                var emailSendingResult = await emailSendingBL.Execute();
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
    
    public async Task<ServiceResponse<bool>> ResetPassword()
    {
        throw new NotImplementedException();
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

    public async Task<ServiceResponse<GetAdminUser>> VerifyAdminUser(int userId)
    {
        var response = new ServiceResponse<GetAdminUser>();

        var targetUser = await _userRepository.GetUserByID(userId);

        if (targetUser == null)
        {
            response.IsSuccess = false;
            response.Data = null;
        }
        else
        {
            var user = new GetAdminUser
            {
                UserId = targetUser.UserId,
                UserRoleName = Enum.GetName(typeof(UserRoleType), targetUser.UserRoleId) ?? "Unknown",
            };

            if (targetUser.UserRoleId == (int)UserRoleType.Admin)
            {
                response.IsSuccess = true;
                response.Data = user;
            }
            else if (targetUser.UserRoleId == (int)UserRoleType.SuperAdmin)
            {
                response.IsSuccess = true;
                response.Data = user;
                response.ErrorMessage = "Operation authorized *NOTE* as user was SuperAdmin though not Admin";
            }
            else if (targetUser.UserRoleId == (int)UserRoleType.User)
            {
                response.IsSuccess = false;
                response.Data = null;
            }
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> VerifyVerificationCode(string email, string code)
    {
        var result = new ServiceResponse<bool>();

        var verificationCode = await _verificationCodeRepository
            .GetVerificationCode(v => v.Email == email && v.Code == code && !v.IsUsed);

        bool isActive = verificationCode == null ? false : verificationCode!.IsActive();

        if (verificationCode == null || verificationCode.Code != code)
        {
            result.ErrorMessage = "Verification code is not correct";
            result.IsSuccess = false;
            result.Data = false;
        }
        else if (!isActive)
        {
            result.ErrorMessage = "Verification code is expired";
            result.IsSuccess = false;
            result.Data = false;
        }
        else
        {
            var targetUser = await _userRepository.GetUserByEmail(email);

            if (targetUser != null && !targetUser.IsVerified)
            {
                bool isUserVerified  = await _userRepository.UpdateUser(new User 
                {
                    UserId = targetUser.UserId,
                    IsVerified = true 
                });

                if (isUserVerified)
                {
                    bool verificationCodeMarkedUsed = await _verificationCodeRepository
                        .MarkVerificationCodeAsUsed(verificationCode.VerificationCodeId);

                    var emailSendingBL = new SMTPEmailSendingBusinessLogic(_userRepository, _smtpEmailSender, targetUser.UserId, "AUTHORIZATION", "Verification was successful");

                    var emailSendingResult = await emailSendingBL.Execute();

                    if (verificationCodeMarkedUsed)
                    {
                        result.IsSuccess = true;
                        result.Data = true;
                    }
                }
            }
            else if (targetUser!.IsVerified)
            {
                result.IsSuccess = false;
                result.Data = false;
                result.ErrorMessage = "User is already verified";
            }
            else
            {
                result.IsSuccess = false;
                result.Data = false;
            }
        }

        return result;
    }
    #endregion
}
