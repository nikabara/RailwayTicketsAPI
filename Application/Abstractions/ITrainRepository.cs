using Domain.Entities;

namespace Application.Abstractions;

public interface ITrainRepository
{
    public Task<bool> RemoveTrain(int id);
    public Task<Train?> GetTrainByID(int id);
    public Task<int?> AddTrain(Train train);
    public Task<bool> UpdateTrain(Train train);
}