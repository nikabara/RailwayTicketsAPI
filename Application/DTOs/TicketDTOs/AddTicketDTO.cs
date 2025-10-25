namespace Application.DTOs.TicketDTOs;

public class AddTicketDTO
{
    public int UserId { get; set; }
    public int SeatId { get; set; }
    public DateTime DateOfBooking { get; set; } = DateTime.Now; 
    public decimal TicketPrice { get; set; }
}
