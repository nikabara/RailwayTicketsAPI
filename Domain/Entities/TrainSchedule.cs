namespace Domain.Entities;

public class TrainSchedule
{
    #region Properties
    public int TrainScheduleId { get; set; }
    public int? TrainId { get; set; }
    public string? DepartureFrom { get; set; } = string.Empty;
    public string? ArrivalAt { get; set; } = string.Empty;
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    #endregion

    #region Configuration Properties
    public virtual Train Train { get; set; } = new();
    public virtual ICollection<Transaction> Transactions { get; set; } = [];
    #endregion
}
