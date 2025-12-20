using Application.Abstractions;
using Application.DTOs.VagonDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class VagonFilterService : IVagonFilterService
{
    #region Properties
    private readonly IVagonFilterRepository _vagonFilterRepository;
    #endregion

    #region Constructors
    public VagonFilterService(IVagonFilterRepository vagonFilterRepository)
    {
        _vagonFilterRepository = vagonFilterRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<List<GetVagonDTO>>> GetFilteredVagons(VagonFilterDTO filterOptions)
    {
        var response = new ServiceResponse<List<GetVagonDTO>>();

        var filteredVagons = (await _vagonFilterRepository.FilterVagons(filterOptions))
            .Select(v => new GetVagonDTO
            {
                VagonId = v.VagonId,
                TrainId = v.TrainId,
                Capacity = v.Capacity,
                VagonType = v.VagonType
            })
            .ToList();

        if (filteredVagons == null)
        {
            response.ErrorMessage = "Error retrieving vagons.";
            response.IsSuccess = false;
        }
        else
        {
            response.IsSuccess = true;
            response.Data = filteredVagons;
        }

        return response;
    }

    #endregion
}
