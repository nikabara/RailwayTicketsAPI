using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<int?> AddUser(User user)
    {
        var result = default(int?);

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        result = user.UserId;

        return result;
    }

    public async Task<User?> GetUserByID(int id)
    {
        var result = new User();

        result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

        return result;
    }

    public async Task<bool> RemoveUser(int id)
    {
        var result = default(bool);

        var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

        if (targetUser == null)
        {
            result = false;
        }
        else
        {
            _dbContext.Users.Remove(targetUser);
            int rowsAffected = await _dbContext.SaveChangesAsync();

            result = rowsAffected > 0;
        }

        return result;
    }

    public async Task<bool> UpdateUser(User user)
    {
        var result = default(bool);

        var targetUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);

        if (targetUser == null)
        {
            result = false;
        }
        else
        {
            targetUser.Name = user.Name == string.Empty
                ? targetUser.Name
                : user.Name;

            targetUser.LastName = user.LastName == string.Empty
                ? targetUser.LastName
                : user.LastName;

            targetUser.Age = user.Age == default
                ? targetUser.Age
                : user.Age;

            targetUser.Email = user.Email == string.Empty
                ? targetUser.Email 
                : user.Email;

            targetUser.PhoneNumber = !string.IsNullOrWhiteSpace(user.PhoneNumber)
                ? user.PhoneNumber
                : targetUser.PhoneNumber;

            result = true;

            await _dbContext.SaveChangesAsync();
        }

        return result;
    }

    #endregion
}
