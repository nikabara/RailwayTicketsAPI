namespace Application.DTOs.UserDTO;

public class UserFilterDTO
{
    public int? UserId { get; set; }
    public int? UserRoleId { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public int? Age { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
}
