using Application.DTOs.TrainDTOs;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface ITrainService
{
    Task<ServiceResponse<GetTrainDTO>> GetTrainByID(int id);
    Task<ServiceResponse<int>> AddTrain(AddTrainDTO addTrainDTO);
    Task<ServiceResponse<bool>> RemoveTrain(int id);
    Task<ServiceResponse<bool>> UpdateTrain(UpdateTrainDTO updateTrainDTO);
}
