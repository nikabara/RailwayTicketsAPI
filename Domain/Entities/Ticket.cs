namespace Domain.Entities;

public class Ticket
{
    #region Properties
    public int TicketId { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public int? UserId { get; set; }
    public int? SeatId { get; set; }
    public DateTime? DateOfBooking { get; set; }
    public decimal? TicketPrice { get; set; }
    public int? TicketPaymentStatusId { get; set; }
    #endregion


    #region Properties
    public virtual Seat Seat { get; set; } = new();
    public virtual User User { get; set; } = new();
    public virtual PaymentStatus PaymentStatus { get; set; } = new();
    #endregion
}
