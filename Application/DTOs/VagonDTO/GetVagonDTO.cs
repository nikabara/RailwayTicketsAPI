using Application.DTOs.SeatDTOs;
using Domain.Enums;

namespace Application.DTOs.VagonDTO;

public class GetVagonDTO
{
    public int VagonId { get; set; }
    public int? TrainId { get; set; }
    public int? Capacity { get; set; }
    public VagonType? VagonType { get; set; }
    public List<GetSeatDTO>? Seats { get; set; }
}
