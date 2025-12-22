using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Transaction
{
    #region Properties
    [Key]
    public int TransactionId { get; set; }
    public int TransactionStateId { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set; }
    public int TrainScheduleId { get; set; }
    public int CreditCardId { get; set; }
    public int CurrencyId { get; set; }
    public decimal TransactionAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsActive { get; set; } = true;
    #endregion

    #region Navigation properties
    public virtual TransactionState? TransactionState { get; set; }
    public virtual User? User { get; set; }
    public virtual Seat? Seat { get; set; }
    public virtual TrainSchedule? TrainSchedule { get; set; }
    public virtual Currency? Currency { get; set; }
    public virtual CreditCard? CreditCard { get; set; }
    #endregion
}
