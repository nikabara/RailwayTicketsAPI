namespace Domain.Entities;

public class UserCreditCard
{
    #region Properties
    public int UserId { get; set; }
    public int CreditCardId { get; set; }
    #endregion

    #region Navigation properties
    public virtual User User { get; set; } = new();
    public virtual CreditCard CreditCard { get; set; } = new();
    #endregion
}
