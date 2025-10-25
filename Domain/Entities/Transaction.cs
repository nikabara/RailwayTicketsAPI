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
    public int CurrencyId { get; set; }
    public decimal TransactionPrice { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsActive { get; set; } = true;
    #endregion

    #region Navigation properties
    public virtual TransactionState TransactionState { get; set; } = new();
    public virtual User User { get; set; } = new();
    public virtual Seat Seat { get; set; } = new();
    public virtual TrainSchedule TrainSchedule { get; set; } = new();
    public virtual Currency Currency { get; set; } = new();
    #endregion
}
