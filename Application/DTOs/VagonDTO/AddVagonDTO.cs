using Domain.Enums;

namespace Application.DTOs.VagonDTO;

public class AddVagonDTO
{
    public int TrainId { get; set; }
    public VagonType VagonType { get; set; }
}
