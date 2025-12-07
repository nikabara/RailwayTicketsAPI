namespace Application.DTOs.TrainScheduleDTOs;

public class TrainAndScheduleFilterDTO
{
    public int? TrainId { get; set; }
    public string? TrainName { get; set; } = string.Empty;
    public int? TrainNumber { get; set; }
    public string? DepartureFrom { get; set; } = string.Empty;
    public string? ArrivalAt { get; set; } = string.Empty;
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
}
