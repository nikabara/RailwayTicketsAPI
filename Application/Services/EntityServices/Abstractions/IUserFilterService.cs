using Application.DTOs.UserDTO;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface IUserFilterService
{
    public Task<ServiceResponse<List<UserFilterDTO>>> FilterUsers(UserFilterDTO filterOptions);
}
