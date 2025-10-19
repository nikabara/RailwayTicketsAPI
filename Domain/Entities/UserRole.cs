using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserRole
{
    #region Properties
    [Key]
    public int UserRoleId { get; set; }
    public string UserRoleName { get; set; } = string.Empty;
    #endregion

    #region Navigation Properties
    public virtual List<User> Users { get; set; } = new();
    #endregion
}