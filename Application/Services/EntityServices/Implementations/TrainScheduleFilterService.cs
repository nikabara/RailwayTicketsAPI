using Application.Abstractions;
using Application.DTOs.TrainScheduleDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class TrainScheduleFilterService : ITrainScheduleFilterService
{
    #region Properties
    private readonly ITrainScheduleRepository _trainScheduleRepository;
    #endregion

    #region Constructor
    public TrainScheduleFilterService(ITrainScheduleRepository trainScheduleRepository)
    {
        _trainScheduleRepository = trainScheduleRepository;
    }
    #endregion

    #region Methods
    public async Task<ServiceResponse<List<TrainAndScheduleFilterDTO>>> Filter(TrainAndScheduleFilterDTO filterOptions)
    {
        var response = new ServiceResponse<List<TrainAndScheduleFilterDTO>>();

        Train train = new Train
        {
            TrainName = filterOptions.TrainName,
            TrainNumber = filterOptions.TrainNumber
        };

        //List<Train> targetTrains = await _trainService.GetTrainsByValue(train);

        TrainSchedule trainSchedule = new TrainSchedule
        {
            DepartureFrom = filterOptions.DepartureFrom,
            ArrivalAt = filterOptions.ArrivalAt,
            DepartureDate = filterOptions.DepartureDate,
            ArrivalDate = filterOptions.ArrivalDate
        };

        var filteredSchedules = await _trainScheduleRepository.FilterTrainSchedules(trainSchedule, filterOptions.TrainName!, filterOptions.TrainNumber);

        if (filteredSchedules == null)
        {
            response.ErrorMessage = "No train schedules found matching the filter criteria.";
            response.IsSuccess = false;
        }
        else
        {
            response.IsSuccess = true;

            var mergedData = new List<TrainAndScheduleFilterDTO>();

            foreach (var ts in filteredSchedules)
            {
                mergedData.Add(new TrainAndScheduleFilterDTO
                {
                    TrainName = ts.Train!.TrainName,
                    TrainNumber = ts.Train.TrainNumber,
                    DepartureFrom = ts.DepartureFrom,
                    ArrivalAt = ts.ArrivalAt,
                    DepartureDate = ts.DepartureDate,
                    ArrivalDate = ts.ArrivalDate
                });
            }

            response.Data = mergedData;
        }

        return response;
    }
    #endregion
}
