using Application.Abstractions;
using Application.DTOs.TrainDTOs;
using Application.DTOs.TrainScheduleDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class TrainScheduleService : ITrainScheduleService
{
    #region Properties
    private readonly ITrainScheduleRepository _trainScheduleRepository;
    private readonly ITrainRepository _trainRepository;
    #endregion

    #region Constructors
    public TrainScheduleService(ITrainScheduleRepository trainScheduleRepository, ITrainRepository trainRepository)
    {
        _trainScheduleRepository = trainScheduleRepository;
        _trainRepository = trainRepository;
    }
    #endregion

    #region Methods
    public async Task<ServiceResponse<int?>> AddTrainSchedule(AddTrainScheduleDTO trainScheduleDTO)
    {
        var response = new ServiceResponse<int?>();

        var targetTrain = await _trainRepository.GetTrainByID(trainScheduleDTO.TrainId);
        
        if (targetTrain == null)
        {
            response.ErrorMessage = "Train returned as null, cannot add train schedule without a valid train";
            response.IsSuccess = false;
        }
        else
        {
            var trainSchedule = new TrainSchedule
            {
                TrainId = trainScheduleDTO.TrainId,
                DepartureDate = trainScheduleDTO.DepartureDate,
                ArrivalDate = trainScheduleDTO.ArrivalDate,
                DepartureFrom = trainScheduleDTO.DepartureFrom,
                ArrivalAt = trainScheduleDTO.ArrivalAt,
                Train = targetTrain!
            };

            int? addedTrainScheduleId = await _trainScheduleRepository.AddTrainSchedule(trainSchedule);

            if (addedTrainScheduleId != null || addedTrainScheduleId > 0)
            {
                response.IsSuccess = true;
                response.Data = (int)addedTrainScheduleId;
            }
            else
            {
                response.ErrorMessage = "Error adding train schedule to database, train schedule id either null or less then 0 returned";
                response.IsSuccess = false;
            }
        }

        return response;
    }

    public async Task<ServiceResponse<GetTrainScheduleDTO?>> GetTrainScheduleByID(int id)
    {
        var response = new ServiceResponse<GetTrainScheduleDTO>();

        var trainSchedule = await _trainScheduleRepository.GetTrainScheduleByID(id);

        if (trainSchedule == null)
        {
            response.ErrorMessage = "Train returned as null";
            response.IsSuccess = false;
        }
        else
        {
            var trainScheduleDTO = new GetTrainScheduleDTO()
            {
                TrainScheduleId = trainSchedule.TrainScheduleId,
                TrainId = trainSchedule.TrainId,
                DepartureDate = trainSchedule.DepartureDate,
                ArrivalDate = trainSchedule.ArrivalDate,
                DepartureFrom = trainSchedule.DepartureFrom,
                ArrivalAt = trainSchedule.ArrivalAt
            };

            response.IsSuccess = true;
            response.Data = trainScheduleDTO;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> RemoveTrainSchedule(int id)
    {
        var response = new ServiceResponse<bool>();

        bool isTrainScheduleRemoved = await _trainScheduleRepository.RemoveTrainSchedule(id);

        if (isTrainScheduleRemoved)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error removing Train Schedule with an id : {id}";
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> UpdateTrainSchedule(UpdateTrainScheduleDTO trainScheduleDTO)
    {
        var response = new ServiceResponse<bool>();

        var trainSchedule = new TrainSchedule
        {
            TrainScheduleId = trainScheduleDTO.TrainScheduleId,
            TrainId = trainScheduleDTO.TrainId,
            DepartureDate = trainScheduleDTO.DepartureDate,
            ArrivalDate = trainScheduleDTO.ArrivalDate,
            DepartureFrom = trainScheduleDTO.DepartureFrom,
            ArrivalAt = trainScheduleDTO.ArrivalAt
        };

        bool isTrainScheduleEdited = await _trainScheduleRepository.UpdateTrainSchedule(trainSchedule);

        if (isTrainScheduleEdited)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = "Error occured while updating train Schedule";
        }

        return response;
    }

    #endregion
}
