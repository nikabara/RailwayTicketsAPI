using Application.Abstractions;
using Application.DTOs.TrainDTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainFilterRepository : ITrainFilterRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public TrainFilterRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<List<Train>> GetFilteredTrains(TrainFilterDTO filterOptions)
    {
        IQueryable<Train> query = _dbContext.Trains;

        if (filterOptions.TrainId != null)
        {
            query = query.Where(t => t.TrainId == filterOptions.TrainId);
        }

        if (filterOptions.TrainNumber != null)
        {
            query = query.Where(t => t.TrainNumber == filterOptions.TrainNumber);
        }

        if (!string.IsNullOrWhiteSpace(filterOptions.TrainName))
        {
            query = query.Where(t => t.TrainName!.Contains(filterOptions.TrainName));
        }

        return await query.ToListAsync();
    }
    #endregion
}
