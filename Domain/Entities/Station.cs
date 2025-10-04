using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Station
{
    [Key]
    public int StationId { get; set; }
    public string StationName { get; set; } = string.Empty;
    public int StationNumber { get; set; }
}
