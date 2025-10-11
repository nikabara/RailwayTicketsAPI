using Domain.Enums;

namespace Application.DTOs.VagonDTO;

public class GetVagonDTO
{
    public int VagonId { get; set; }
    public int? TrainId { get; set; }
    public VagonType? VagonType { get; set; }
}
