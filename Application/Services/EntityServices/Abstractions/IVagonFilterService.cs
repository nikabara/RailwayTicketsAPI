using Application.DTOs.VagonDTO;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface IVagonFilterService
{
    public Task<ServiceResponse<List<GetVagonDTO>>> GetFilteredVagons(VagonFilterDTO filterOptions);
}
