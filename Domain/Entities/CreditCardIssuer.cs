using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class CreditCardIssuer
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CreditCardIssuerId { get; set; }
    public string CreditCardIssuerName { get; set; } = string.Empty;

    #region Navigation Properties
    public virtual List<CreditCard> CreditCards { get; set; } = new();
    #endregion
}