namespace Application.DTOs.TicketDTOs;

public class GetTicketDTO
{
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public int SeatId { get; set; }
    public DateTime DateOfBooking { get; set; }
    public decimal TicketPrice { get; set; }
}
