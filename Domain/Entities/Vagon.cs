using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Vagon
{
    #region Properties
    [Key]
    public int VagonId { get; set; }
    public int TrainId { get; set; }
    public VagonType VagonType { get; set; }
    #endregion

    #region Configuration Properties
    public virtual Train Train { get; set; } = new();
    public virtual ICollection<Seat> Seats { get; set; } = [];
    #endregion
}
