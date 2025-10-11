using Domain.Entities;

namespace Application.Abstractions;

public interface IVagonRepository
{
    public Task<int?> AddVagon(Vagon vagon);
    public Task<bool> RemoveVagon(int id);
    public Task<Vagon?> GetVagonByID(int id);
    public Task<bool> UpdateVagon(Vagon vagon);
}
