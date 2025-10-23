using Application.Abstractions;
using Application.BusinessLogics;
using Application.DTOs.VagonDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class VagonService : IVagonService
{
    #region Properties
    private readonly IVagonRepository _vagonRepository;
    private readonly ITrainRepository _trainRepository;
    private readonly ISeatRepository _seatRepository;
    #endregion

    #region Constructors
    public VagonService(IVagonRepository vagonRepository, ITrainRepository trainRepository, ISeatRepository seatRepository)
    {
        _vagonRepository = vagonRepository;
        _trainRepository = trainRepository;
        _seatRepository = seatRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<int?>> AddVagon(AddVagonDTO vagonDTO)
    {
        var response = new ServiceResponse<int?>();

        var targetTrain = await _trainRepository.GetTrainByID(vagonDTO.TrainId);

        if (targetTrain == null)
        {
            response.IsSuccess = false;
            response.ErrorMessage = "No train with given Train Id was found";
        }
        else
        {
            var vagon = new Vagon
            {
                TrainId = vagonDTO.TrainId,
                VagonType = vagonDTO.VagonType,
                Capacity = vagonDTO.Capacity,
                Train = targetTrain
            };

            int? addedVagonId = await _vagonRepository.AddVagon(vagon);

            if (addedVagonId > 0 && addedVagonId != null)
            {
                response.Data = addedVagonId;
                response.IsSuccess = true;
            }
            else
            {
                response.ErrorMessage = "Error while adding vagon to database";
                response.IsSuccess = false;
            }
        }

        return response;
    }
    public async Task<ServiceResponse<int?>> AddVagon2(AddVagonDTO vagonDTO)
    {
        var response = new ServiceResponse<int?>();

        var targetTrain = await _trainRepository.GetTrainByID(vagonDTO.TrainId);

        if (targetTrain == null)
        {
            response.IsSuccess = false;
            response.ErrorMessage = "No train with given Train Id was found";
        }
        else
        {
            var vagon = new Vagon
            {
                TrainId = vagonDTO.TrainId,
                VagonType = vagonDTO.VagonType,
                Capacity = vagonDTO.Capacity,
                Train = targetTrain
            };

            int? addedVagonId = await _vagonRepository.AddVagon(vagon);

            if (addedVagonId > 0 && addedVagonId != null)
            {
                var addVagonSeatsBL = new CreateVagonSeatsBusinessLogic(_seatRepository, _vagonRepository, (int)addedVagonId);

                var result = await addVagonSeatsBL.Execute();

                if (!result.IsError)
                {
                    response.Data = addedVagonId;
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = result.ErrorMessage!;
                }
            }
            else
            {
                response.ErrorMessage = "Error while adding vagon to database";
                response.IsSuccess = false;
            }
        }

        return response;
    }

    public async Task<ServiceResponse<GetVagonDTO?>> GetVagonByID(int id)
    {
        var response = new ServiceResponse<GetVagonDTO?>();

        var vagon = await _vagonRepository.GetVagonByID(id);

        if (vagon == null)
        {
            response.ErrorMessage = "Error fetching vagon from database";
            response.IsSuccess = false;
        }
        else
        {
            var vagonDTO = new GetVagonDTO
            {
                VagonId = vagon.VagonId,
                TrainId = vagon.TrainId,
                Capacity = vagon.Capacity,
                VagonType = vagon.VagonType
            };

            response.Data = vagonDTO;
            response.IsSuccess = true;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> RemoveVagon(int id)
    {
        var response = new ServiceResponse<bool>();

        var isVagonRemoved = await _vagonRepository.RemoveVagon(id);

        if (isVagonRemoved)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error removing Vagon on id : {id}";
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> UpdateVagon(UpdateVagonDTO vagonDTO)
    {
        var response = new ServiceResponse<bool>();

        var vagon = new Vagon
        {
            VagonId = vagonDTO.VagonId,
            TrainId = vagonDTO.TrainId,
            Capacity = vagonDTO.Capacity,
            VagonType = vagonDTO.VagonType
        };

        var isVagonUpdated = await _vagonRepository.UpdateVagon(vagon);

        if (isVagonUpdated)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = "Error updating vagon";
        }

        return response;
    }
    #endregion
}
