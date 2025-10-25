using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class CreditCard
{
    #region Properties
    [Key]
    public int CreditCardId { get; set; }
    public int UserId { get; set; }
    public int CreditCardIssuerId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; } = string.Empty;
    #endregion

    #region Navigation Properties
    public virtual List<UserCreditCard> UserCreditCards { get; set; } = new();
    public virtual CreditCardIssuer CreditCardIssuer { get; set; } = new();
    #endregion
}
