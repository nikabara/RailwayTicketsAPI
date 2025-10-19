using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    #region Properties
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    //public int UserRoleId { get; set; }
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
    public decimal UserBalance { get; set; } = default;
    public string? PhoneNumber { get; set; } = string.Empty;
    public byte[] PasswordSalt { get; set; } = [];
    public byte[] PasswordHash { get; set; } = [];
    public DateTime RegistrationDate { get; set; }
    public bool IsVerified { get; set; } = false;
    public bool IsAdmin { get; set; } = false;
    #endregion

    #region Configuation Properties 
    public virtual ICollection<Ticket> Tickets { get; set; } = [];
    #endregion
}