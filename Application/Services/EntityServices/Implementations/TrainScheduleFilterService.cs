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
    //private readonly ITrainRepository _trainRepository;
    #endregion

    #region Constructor
    public TrainScheduleFilterService(ITrainScheduleRepository trainScheduleRepository/*, ITrainRepository trainRepository*/)
    {
        _trainScheduleRepository = trainScheduleRepository;
        //_trainRepository = trainRepository;
    }
    #endregion

    #region Methods
    public async Task<ServiceResponse<List<ScheduleFilterDTO>>> Filter(ScheduleFilterDTO filterOptions)
    {
        var response = new ServiceResponse<List<ScheduleFilterDTO>>();

        Train train = new Train
        {
            TrainName = filterOptions.TrainName,
            TrainNumber = filterOptions.TrainNumber
        };

        TrainSchedule trainSchedule = new TrainSchedule
        {
            DepartureFrom = filterOptions.DepartureFrom,
            ArrivalAt = filterOptions.ArrivalAt,
            DepartureDate = filterOptions.DepartureDate,
            ArrivalDate = filterOptions.ArrivalDate
        };

        var filteredSchedules = await _trainScheduleRepository.FilterSchedules(trainSchedule, filterOptions.TrainName!, filterOptions.TrainNumber);

        if (filteredSchedules == null)
        {
            response.ErrorMessage = "No train schedules found matching the filter criteria.";
            response.IsSuccess = false;
        }
        else
        {
            response.IsSuccess = true;

            var mergedData = new List<ScheduleFilterDTO>();

            foreach (var ts in filteredSchedules)
            {
                mergedData.Add(new ScheduleFilterDTO
                {
                    ScheduleId = ts.TrainScheduleId,
                    TrainId = ts.Train!.TrainId,
                    TrainName = ts.Train!.TrainName,
                    TrainNumber = ts.Train.TrainNumber,
                    DepartureFrom = ts.DepartureFrom,
                    ArrivalAt = ts.ArrivalAt,
                    DepartureDate = ts.DepartureDate,
                    ArrivalDate = ts.ArrivalDate
                });
            }

            // Only merge trains when train name or number is provided
            //if (trainSchedule.DepartureDate == null 
            //    && trainSchedule.ArrivalDate == null 
            //    && string.IsNullOrWhiteSpace(trainSchedule.DepartureFrom) 
            //    && string.IsNullOrWhiteSpace(trainSchedule.ArrivalAt))
            //{
            //    // Get filtered trains
            //    var filteredTrains = await _trainRepository.GetTrainsByValue(train);

            //    // Get trains which have no shcedules
            //    var targetTrains = filteredTrains
            //        .Where(t => !t.TrainSchedules.Any())
            //        .ToList();

            //    foreach (var t in targetTrains)
            //    {
            //        mergedData.Add(new TrainAndScheduleFilterDTO
            //        {
            //            TrainId = t.TrainId,
            //            TrainName = t.TrainName,
            //            TrainNumber = t.TrainNumber,
            //            DepartureFrom = string.Empty,
            //            ArrivalAt = string.Empty,
            //            DepartureDate = null,
            //            ArrivalDate = null
            //        });
            //    }
            //}

            response.Data = mergedData;
        }

        return response;
    }
    #endregion
}
