using Domain.Entities;

namespace Domain.Abstractions;

public interface ITrainRepository
{
    public Task<int?> AddTrain(Train train);
    public Task<Train?> GetTrainByID(int id);
    public Task<bool> RemoveTrain(int id);
}
