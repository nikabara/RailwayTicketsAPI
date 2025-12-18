using Application.DTOs.TrainDTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Abstractions;

public interface ITrainFilterService
{
    public Task<ServiceResponse<List<Train>>> GetFilteredTrains(TrainFilterDTO filterOptions);
}
