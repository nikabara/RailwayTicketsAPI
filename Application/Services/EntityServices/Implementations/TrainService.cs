using Application.Abstractions;
using Application.DTOs.TrainDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class TrainService : ITrainService
{
    #region Properties
    private readonly ITrainRepository _trainRepository;
    #endregion

    #region Constructors
    public TrainService(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }
    #endregion

    #region Methods
    public async Task<ServiceResponse<bool>> RemoveTrain(int id)
    {
        var response = new ServiceResponse<bool>();

        bool isTrainRemoved = await _trainRepository.RemoveTrain(id);
        
        if (isTrainRemoved)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error removing train with an id : {id}";
        }

        return response;
    }

    public async Task<ServiceResponse<GetTrainDTO>> GetTrainByID(int id)
    {
        var response = new ServiceResponse<GetTrainDTO>();

        var train = await _trainRepository.GetTrainByID(id);

        if (train == null)
        {
            response.ErrorMessage = "Train returned as null";
            response.IsSuccess = false;    
        }
        else
        {
            var trainDTO = new GetTrainDTO()
            {
                TrainId = train.TrainId,
                TrainName = train.TrainName,
                TrainNumber = train.TrainNumber
            };
        
            response.IsSuccess = true;
            response.Data = trainDTO;
        }

        return response;
    }

    public async Task<ServiceResponse<int>> AddTrain(AddTrainDTO addTrainDTO)
    {
        var response = new ServiceResponse<int>();

        var train = new Train()
        {
            TrainName = addTrainDTO.TrainName,
            TrainNumber = addTrainDTO.TrainNumber
        };
        
        int? addedTrainId = await _trainRepository.AddTrain(train);
        
        if (addedTrainId != null && addedTrainId > 0)
        {
            response.IsSuccess = true;
            response.Data = (int)addedTrainId;
        }
        else
        {
            response.ErrorMessage = "Error adding train to database, train id either null or less then 0 returned";
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> UpdateTrain(UpdateTrainDTO updateTrainDTO)
    {
        var response = new ServiceResponse<bool>();

        var train = new Train
        {
            TrainId = updateTrainDTO.TrainId,
            TrainName = updateTrainDTO.TrainName,
            TrainNumber = updateTrainDTO.TrainNumber
        };

        bool isTrainEdited = await _trainRepository.UpdateTrain(train);

        if (isTrainEdited)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = "Error occured while updating train";
        }

        return response;
    }
    #endregion
}