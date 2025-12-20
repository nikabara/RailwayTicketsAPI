using Application.DTOs.VagonDTO;
using Domain.Entities;

namespace Application.Abstractions;

public interface IVagonFilterRepository
{
    public Task<List<Vagon>> FilterVagons(VagonFilterDTO filterOptions);
}
