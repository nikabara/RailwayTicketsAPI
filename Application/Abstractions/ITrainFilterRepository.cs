using Application.DTOs.TrainDTOs;
using Domain.Entities;

namespace Application.Abstractions;

public interface ITrainFilterRepository
{
    public Task<List<Train>> GetFilteredTrains(TrainFilterDTO filterOptions);
}
