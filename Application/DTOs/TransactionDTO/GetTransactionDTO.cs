using Application.DTOs.CreditCardDTOs;
using Application.DTOs.SeatDTOs;
using Application.DTOs.TrainScheduleDTOs;
using Domain.Entities;

namespace Application.DTOs.TransactionDTO;

public class GetTransactionDTO
{
    public int TransactionId { get; set; }
    public string TransactionState { get; set; } = string.Empty;
    public int SeatId { get; set; }
    public int TrainScheduleId { get; set; }
    public int CreditCardId { get; set; }
    public int CurrencyId { get; set; }
    public decimal TransactionAmount { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual GetSeatDTO Seat { get; set; } = new();
    public virtual GetTrainScheduleDTO TrainSchedule { get; set; } = new();
    //public virtual Currency Currency { get; set; } = new();
    public virtual GetCreditCardDTO CreditCard { get; set; } = new();
}
