using Application.DTOs.TrainDTOs;
using Application.Services.Abstractions;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.Implementations;

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
    public async Task<ServiceResponse<GetTrainDTO>> GetTrainByID(int id)
    {
        var response = new ServiceResponse<GetTrainDTO>();

        try
        {
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

            
        }
        catch (Exception msg)
        {
            response.ErrorMessage = $"An error occured : {msg}";
            response.IsSuccess = false;
        }
        
        return response;
    }

    public async Task<ServiceResponse<int>> AddTrain(AddTrainDTO addTrainDTO)
    {
        var response = new ServiceResponse<int>();

        try
        {
            var train = new Train()
            {
                TrainName = addTrainDTO.TrainName,
                TrainNumber = addTrainDTO.TrainNumber
            };

            int? addedTrainId = await _trainRepository.AddTrain(train);

            if (addedTrainId != null || addedTrainId > 0)
            {
                response.IsSuccess = true;
                response.Data = (int)addedTrainId;
            }
            else
            {
                response.ErrorMessage = "Error adding train to database, train id either null or less then 0 returned";
                response.IsSuccess = false;
            }
        }
        catch (Exception msg)
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error occured : {msg}";
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> RemoveTrain(int id)
    {
        var response = new ServiceResponse<bool>();

        try
        {
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
        }
        catch (Exception msg)
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error occured while deleting train from database : {msg}";
        }

        return response;
    }
    #endregion
}
