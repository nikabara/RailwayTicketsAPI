using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Currency
{
    #region Properties
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CurrencyId { get; set; }
    public string CurrencyName { get; set; } = string.Empty;
    #endregion

    #region Navigation Properties
    public virtual List<Transaction> Transactions { get; set; } = new();
    #endregion
}
