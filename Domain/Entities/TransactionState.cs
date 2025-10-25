using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class TransactionState
{
    #region Properties
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int TransactionStateId { get; set; }
    public string TransactionStateName { get; set; } = string.Empty;
    #endregion

    #region Navigation Properties
    public virtual List<Transaction> Transactions { get; set; } = new();
    #endregion
}
