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
            UserRoleId = (int)addUserDTO.UserRoleType
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
                UserRoleId = user.UserRoleId,
                Name = user.Name,
                LastName = user.LastName,
                Age = user.Age,
                Email = user.Email,
                UserBalance = user.UserBalance,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                RegistrationDate = user.RegistrationDate,
                IsVerified = user.IsVerified
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
            Email = updateUserDTO.Email!,
            UserRoleId = (int)updateUserDTO.UserRoleType,
            IsVerified = updateUserDTO.isVerified
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

    /// <summary>
    /// Add given funds to balance of the user with given id
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="userId"></param>
    /// <returns><b>decimal</b> amount of new updated balance</returns>
    public async Task<ServiceResponse<decimal>> AddUserFunds(decimal amount, int userId)
    {
        var response = new ServiceResponse<decimal>();

        var targetUser = await _userRepository.GetUserByID(userId);

        if (targetUser == null)
        {
            response.ErrorMessage = $"User with id : {userId} not found";
            response.IsSuccess = false;
        }
        else
        {
            var newBalance = targetUser.UserBalance + amount;

            var isUpdated = await _userRepository.UpdateUser(new User
            {
                UserId = userId,
                UserBalance = newBalance
            });

            if (isUpdated)
            {
                response.Data = newBalance;
                response.IsSuccess = true;
            }
            else
            {
                response.ErrorMessage = "Error updating user balance";
                response.IsSuccess = false;
            }
        }

        return response;
    }

    /// <summary>
    /// Deducts given funds from balance of the user with given id
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="userId"></param>
    /// <returns><b>decimal</b> amount of new updated balance</returns>
    public async Task<ServiceResponse<decimal>> DeductUserFunds(decimal amount, int userId)
    {
        var response = new ServiceResponse<decimal>();

        var targetUser = await _userRepository.GetUserByID(userId);

        if (targetUser == null)
        {
            response.ErrorMessage = $"User with id : {userId} not found";
            response.IsSuccess = false;
        }
        else
        {
            var newBalance = targetUser.UserBalance - amount;

            var isUpdated = false;

            if (newBalance > 0)
            {
                isUpdated = await _userRepository.UpdateUser(new User
                {
                    UserId = userId,
                    UserBalance = newBalance
                });
            }
            else
            {
                response.ErrorMessage = "Insufficient funds";
                response.IsSuccess = false;
                return response;
            }

            if (isUpdated)
            {
                response.Data = newBalance;
                response.IsSuccess = true;
            }
            else
            {
                response.ErrorMessage = "Error updating user balance";
                response.IsSuccess = false;
            }
        }

        return response;
    }

    #endregion
}
