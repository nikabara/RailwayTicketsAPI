using Domain.Entities;

namespace Application.Abstractions;

public interface ITrainScheduleRepository
{
    public Task<bool> RemoveTrainSchedule(int id);
    public Task<TrainSchedule?> GetTrainScheduleByID(int id);
    public Task<int?> AddTrainSchedule(TrainSchedule trainSchedule);
    public Task<bool> UpdateTrainSchedule(TrainSchedule trainSchedule);
}