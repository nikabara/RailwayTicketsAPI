using Application.Abstractions;
using Application.DTOs.UserDTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserFilterRepository : IUserFilterRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public UserFilterRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<List<User>> FilterUsers(UserFilterDTO filterOptions)
    {
        IQueryable<User> userQuery = _dbContext.Users;

        if (filterOptions.UserId != null)
        {
            userQuery = userQuery.Where(u => u.UserId == filterOptions.UserId);
        }

        if (filterOptions.UserRoleId != null)
        {
            userQuery = userQuery.Where(u => u.UserRoleId == filterOptions.UserRoleId);
        }

        if (!string.IsNullOrWhiteSpace(filterOptions.Name))
        {
            userQuery = userQuery.Where(u => u.Name.Contains(filterOptions.Name));
        }

        if (!string.IsNullOrWhiteSpace(filterOptions.Name))
        {
            userQuery = userQuery.Where(u => u.Name.Contains(filterOptions.LastName!));
        }

        if (filterOptions.Age != null)
        {
            userQuery = userQuery.Where(u => u.Age == filterOptions.Age);
        }

        if (!string.IsNullOrWhiteSpace(filterOptions.Email))
        {
            userQuery = userQuery.Where(u => u.Email.Contains(filterOptions.Email));
        }

        if (!string.IsNullOrWhiteSpace(filterOptions.PhoneNumber))
        {
            userQuery = userQuery.Where(u => u.PhoneNumber!.Contains(filterOptions.PhoneNumber));
        }

        return await userQuery.ToListAsync();
    }

    #endregion
}
