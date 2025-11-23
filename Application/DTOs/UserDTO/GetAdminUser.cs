namespace Application.DTOs.UserDTO;

public class GetAdminUser
{
    public int UserId { get; set; }
    public string UserRoleName { get; set; } = string.Empty;
}
