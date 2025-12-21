using Application.DTOs.VagonDTO;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.TrainDTOs;

public class GetTrainDTO
{
    #region Properties
    public int TrainId { get; set; }
    public int? TrainNumber { get; set; }
    public string? TrainName { get; set; } = string.Empty;
    public List<GetVagonDTO>? Vagons { get; set; }
    #endregion
}
