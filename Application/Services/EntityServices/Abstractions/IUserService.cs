using Application.DTOs.UserDTO;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface IUserService
{
    public Task<ServiceResponse<GetUserDTO?>> GetUserByID(int id);
    public Task<ServiceResponse<int?>> AddUser(AddUserDTO addUserDTO);
    public Task<ServiceResponse<bool>> RemoveUser(int id);
    public Task<ServiceResponse<bool>> UpdateUser(UpdateUserDTO updateUserDTO);
    public Task<ServiceResponse<bool>> MakeUserAdmin(int userId);
}
