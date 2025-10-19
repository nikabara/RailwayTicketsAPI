using Application.DTOs.AuthDTOs;
using Domain.Common;

namespace Application.Services.AuthServices.Abstractions;

public interface IAuthService
{
    public Task<ServiceResponse<int>> RegisterUser(RegisterUserDTO registerUserDTO);
    public Task<ServiceResponse<string>> LogIn(string email, string password);
    public Task<ServiceResponse<bool>> ResetPassword();
    public Task<ServiceResponse<bool>> SendVerificationCode(int userID);
    public Task<ServiceResponse<bool>> VerifyVerificationCode(string email, string code);
}
