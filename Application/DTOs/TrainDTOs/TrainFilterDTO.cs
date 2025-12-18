namespace Application.DTOs.TrainDTOs;

public class TrainFilterDTO
{
    public int? TrainId { get; set; }
    public int? TrainNumber { get; set; }
    public string? TrainName { get; set; } = string.Empty;
}
