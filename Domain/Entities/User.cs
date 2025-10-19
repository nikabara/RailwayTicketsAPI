using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    #region Properties
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public decimal UserBalance { get; set; } = default;
    public string? PhoneNumber { get; set; } = string.Empty;
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
    public DateTime RegistrationDate { get; set; }
    public bool IsVerified { get; set; } = false;
    
    // Custom user role
    public int UserRoleId { get; set; }
    #endregion

    #region Configuation Properties 
    public virtual UserRole UserRole { get; set; } = new();
    public virtual ICollection<Ticket> Tickets { get; set; } = [];
    #endregion
}