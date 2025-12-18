using Application.Abstractions;
using Application.DTOs.TrainDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class TrainFilterService : ITrainFilterService
{
    #region Properties
    private readonly ITrainFilterRepository _trainRepository;
    #endregion

    #region Constructor
    public TrainFilterService(ITrainFilterRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<List<Train>>> GetFilteredTrains(TrainFilterDTO filterOptions)
    {
        var response = new ServiceResponse<List<Train>>();

        var filteredTrains = await _trainRepository.GetFilteredTrains(filterOptions);

        if (!filteredTrains.Any())
        {
            response.ErrorMessage = "No trains found matching the filter criteria.";
            response.IsSuccess = false;
        }
        else
        {
            response.Data = filteredTrains;
            response.IsSuccess = true;
        }

        return response;
    }

    #endregion
}
