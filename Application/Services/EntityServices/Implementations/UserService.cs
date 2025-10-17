using Application.Abstractions;
using Application.DTOs.UserDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class UserService : IUserService
{
    #region Properties
    private readonly IUserRepository _userRepository;
    #endregion

    #region Constructors
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<int?>> AddUser(AddUserDTO addUserDTO)
    {
        var response = new ServiceResponse<int?>();

        var user = new User
        {
            Name = addUserDTO.Name,
            LastName = addUserDTO.LastName,
            Age = addUserDTO.Age,
            Email = addUserDTO.Email,
            UserBalance = addUserDTO.UserBalance,
            PhoneNumber = addUserDTO.PhoneNumber,
            PasswordSalt = addUserDTO.PasswordSalt,
            PasswordHash = addUserDTO.PasswordHash,
            RegistrationDate = addUserDTO.RegistrationDate,
            IsVerified = addUserDTO.IsVerified,
            IsAdmin = addUserDTO.IsAdmin
        };

        int? addedUserId = await _userRepository.AddUser(user);

        if (addedUserId != null && addedUserId > 0)
        {
            response.IsSuccess = true;
            response.Data = addedUserId;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = "Error adding user";
        }

        return response;
    }

    public async Task<ServiceResponse<GetUserDTO?>> GetUserByID(int id)
    {
        var response = new ServiceResponse<GetUserDTO?>();

        var user = await _userRepository.GetUserByID(id);

        if (user == null)
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"User with id : {id} not found";
        }
        else
        {
            var userDTO = new GetUserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                LastName = user.LastName,
                Age = user.Age,
                Email = user.Email,
                UserBalance = user.UserBalance,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                RegistrationDate = user.RegistrationDate,
                IsVerified = user.IsVerified,
                IsAdmin = user.IsAdmin
            };

            response.IsSuccess = true;
            response.Data = userDTO;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> RemoveUser(int id)
    {
        var response = new ServiceResponse<bool>();

        bool isRemoved = await _userRepository.RemoveUser(id);

        if (isRemoved)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error removing user with id : {id}";
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> UpdateUser(UpdateUserDTO updateUserDTO)
    {
        var response = new ServiceResponse<bool>();

        var user = new User
        {
            UserId = updateUserDTO.UserId,
            Name = updateUserDTO.Name!,
            LastName = updateUserDTO.LastName!,
            Age = updateUserDTO.Age ?? default,
            PhoneNumber = updateUserDTO.PhoneNumber,
            Email = updateUserDTO.Email!
        };

        bool isUserEdited = await _userRepository.UpdateUser(user);

        if (isUserEdited)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error updating user {user.Name} {user.LastName} email : {user.Email}";
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> MakeUserAdmin(int userId)
    {
        var response = new ServiceResponse<bool>();

        bool becameAdmin = await _userRepository.MakeUserAdmin(userId);

        if (becameAdmin == true)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error making user with id : {userId} an admin";
        }

        return response;
    }

    #endregion
}
