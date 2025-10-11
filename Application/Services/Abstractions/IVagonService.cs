using Application.DTOs.VagonDTO;
using Domain.Common;

namespace Application.Services.Abstractions;

public interface IVagonService
{
    public Task<ServiceResponse<int?>> AddVagon(AddVagonDTO vagonDTO);
    public Task<ServiceResponse<bool>> RemoveVagon(int id);
    public Task<ServiceResponse<GetVagonDTO?>> GetVagonByID(int id);
    public Task<ServiceResponse<bool>> UpdateVagon(UpdateVagonDTO vagonDTO);
}
