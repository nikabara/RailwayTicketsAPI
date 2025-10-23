using Domain.Enums;

namespace Application.DTOs.VagonDTO;

public class UpdateVagonDTO
{
    public int VagonId { get; set; }
    public int? TrainId { get; set; }
    public int? Capacity { get; set; }
    public VagonType VagonType { get; set; }
}
