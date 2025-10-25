namespace Application.DTOs.TicketDTOs;

public class UpdateTicketDTO
{
    public int? UserId { get; set; }
    public int? SeatId { get; set; }
    public decimal? TicketPrice { get; set; }
}
