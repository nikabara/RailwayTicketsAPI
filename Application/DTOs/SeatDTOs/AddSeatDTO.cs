namespace Application.DTOs.SeatDTOs;

public class AddSeatDTO
{
    public int SeatId { get; set; }
    public int VagonId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public decimal SeatPrice { get; set; }
    public int SeatStatusId { get; set; }
}
