using Application.Abstractions;
using Application.AuthServices.Abstractions;
using Application.Common;
using Application.DTOs.AuthDTOs;
using Application.ExternalServices.EmailSendingService.Abstractions;
using Domain.Common;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.AuthServices.Implementations;

public class AuthService : IAuthService
{
    #region Properties
    private readonly IUserRepository _userRepository;
    private readonly ISMTPEmailSender _smtpEmailSender;
    private readonly IConfiguration _configuration;
    #endregion

    #region Constructor
    public AuthService(IUserRepository userRepository, ISMTPEmailSender smtpEmailSender, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _smtpEmailSender = smtpEmailSender;
        _configuration = configuration;
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

    public Task<ServiceResponse<bool>> SendVerificationCode(int userID)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<bool>> VerifyVerificationCode(string email, string code)
    {
        throw new NotImplementedException();
    }
    #endregion
}
