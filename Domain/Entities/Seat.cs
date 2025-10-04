namespace Domain.Entities;

public class Seat
{
    #region Properties
    public int SeatId { get; set; }
    public int VagonId { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
    public decimal SeatPrice { get; set; }
    public bool IsOccupied { get; set; }
    #endregion

    #region Configuration Properties
    public virtual Vagon Vagon { get; set; } = new();
    public virtual List<Ticket> Tickets { get; set; } = new();
    #endregion
}
