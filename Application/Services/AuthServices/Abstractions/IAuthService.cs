using Application.DTOs.AuthDTOs;
using Application.DTOs.UserDTO;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.AuthServices.Abstractions;

public interface IAuthService
{
    public Task<ServiceResponse<int>> RegisterUser(RegisterUserDTO registerUserDTO);
    public Task<ServiceResponse<string>> LogIn(string email, string password);
    public Task<ServiceResponse<bool>> ResetPassword();
    public Task<ServiceResponse<bool>> SendVerificationCode(int userID);
    public Task<ServiceResponse<bool>> VerifyVerificationCode(string email, string code);
    public Task<ServiceResponse<GetAdminUser>> VerifyAdminUser(int userId);
    public Task<ServiceResponse<bool>> IsUserVerified(int userId);
}
