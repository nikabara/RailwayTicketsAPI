using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class SeatStatus
{
    #region Properties
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public int SeatStatusId { get; set; }
    public string SeatStatusName { get; set; } = string.Empty;
    #endregion

    #region Navigation Properties
    public virtual List<Seat> Seats { get; set; } = new();
    #endregion
}
