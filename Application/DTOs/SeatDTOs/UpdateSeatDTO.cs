namespace Application.DTOs.SeatDTOs;

public class UpdateSeatDTO
{
    public int SeatId { get; set; }
    public int? VagonId { get; set; }
    public string? SeatNumber { get; set; } = string.Empty;
    public decimal? SeatPrice { get; set; }
    public bool? IsOccupied { get; set; }
}
