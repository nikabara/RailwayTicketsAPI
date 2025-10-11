using Application.DTOs.TrainScheduleDTO;
using Domain.Common;

namespace Application.Services.Abstractions;

public interface ITrainScheduleService
{
    public Task<ServiceResponse<bool>> RemoveTrainSchedule(int id);
    public Task<ServiceResponse<GetTrainScheduleDTO?>> GetTrainScheduleByID(int id);
    public Task<ServiceResponse<int?>> AddTrainSchedule(AddTrainScheduleDTO trainSchedule);
    public Task<ServiceResponse<bool>> UpdateTrainSchedule(UpdateTrainScheduleDTO trainSchedule);
}
