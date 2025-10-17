using Domain.Entities;

namespace Application.Abstractions;

public interface IUserRepository
{
    public Task<bool> RemoveUser(int id);
    public Task<User?> GetUserByID(int id);
    public Task<User?> GetUserByEmail(string email);
    public Task<int?> AddUser(User user);
    public Task<bool> UpdateUser(User user);
    public Task<bool> MakeUserAdmin(int id);
}
