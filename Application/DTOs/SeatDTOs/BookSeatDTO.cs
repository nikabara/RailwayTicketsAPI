namespace Application.DTOs.SeatDTOs;

public class BookSeatDTO
{
    public decimal TransactionAmount { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set;  }
    public int TrainScheduleId { get; set; }
    public int CreditCardId { get; set; }
    public int CurrencyId { get; set; }
}
