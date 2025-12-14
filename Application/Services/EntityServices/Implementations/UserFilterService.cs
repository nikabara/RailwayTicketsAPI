using Application.Abstractions;
using Application.DTOs.UserDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;

namespace Application.Services.EntityServices.Implementations;

public class UserFilterService : IUserFilterService
{
    #region Properties
    private readonly IUserFilterRepository _userFilterRepository;
    #endregion

    #region Constructors
    public UserFilterService(IUserFilterRepository userFilterRepository)
    {
        _userFilterRepository = userFilterRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<List<UserFilterDTO>>> FilterUsers(UserFilterDTO filterOptions)
    {
        var response = new ServiceResponse<List<UserFilterDTO>>();

        var filteredUsers = (await _userFilterRepository.FilterUsers(filterOptions))
            .Select(u => new UserFilterDTO
            {
                UserId = u.UserId,
                UserRoleId = u.UserRoleId,
                Name = u.Name,
                LastName = u.LastName,
                Age = u.Age,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ToList();

        if (filteredUsers == null)
        {
            response.ErrorMessage = "No users found matching the filter criteria.";
            response.IsSuccess = false;
        }
        else
        {
            response.IsSuccess = true;
            response.Data = filteredUsers;
        }

        return response;
    }

    #endregion
}
