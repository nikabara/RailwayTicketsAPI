using Application.DTOs.UserDTO;
using Domain.Entities;

namespace Application.Abstractions;

public interface IUserFilterRepository
{
    public Task<List<User>> FilterUsers(UserFilterDTO filterOptions);
}
