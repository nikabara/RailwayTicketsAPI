using Application.Abstractions;
using Application.DTOs.VagonDTO;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VagonFilterRepository : IVagonFilterRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public VagonFilterRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<List<Vagon>> FilterVagons(VagonFilterDTO filterOptions)
    {
        IQueryable<Vagon> query = _dbContext.Vagons.AsQueryable();

        if (filterOptions.VagonId != null)
        {
            query = query.Where(v => v.VagonId == filterOptions.VagonId);
        }

        if (filterOptions.TrainId != null)
        {
            query = query.Where(v => v.TrainId == filterOptions.TrainId.Value);
        }

        if (filterOptions.Capacity != null)
        {
            query = query.Where(v => v.Capacity == filterOptions.Capacity.Value);
        }

        if (filterOptions.VagonType != null)
        {
            query = query.Where(v => (int)v.VagonType == (int)filterOptions.VagonType);
        }

        return await query.ToListAsync();
    }

    #endregion
}