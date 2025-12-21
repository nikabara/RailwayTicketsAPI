using Domain.Enums;

namespace Application.DTOs.UserDTO;

public class UpdateUserDTO
{
    public int UserId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public UserRoleType? UserRoleType { get; set; }
    public bool isVerified { get; set; }
    public decimal UserBalance { get; set; }
}
