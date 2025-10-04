using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Train
{
    #region Properties
    [Key]
    public int TrainId { get; set; }
    public int TrainNumber { get; set; }
    public string TrainName { get; set; } = string.Empty;
    #endregion

    #region Configuration Properties
    public virtual ICollection<Vagon> Vagons { get; set; } = [];
    public virtual ICollection<TrainSchedule> TrainSchedules { get; set; } = [];
    #endregion
}
