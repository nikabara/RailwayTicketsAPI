namespace Application.DTOs.TrainScheduleDTO;

public class AddTrainScheduleDTO
{
    public int TrainId { get; set; }
    public string DepartureFrom { get; set; } = string.Empty;
    public string ArrivalAt { get; set; } = string.Empty;
    public DateTime DepartureDate { get; set; }
    public DateTime ArrivalDate { get; set; }
}
