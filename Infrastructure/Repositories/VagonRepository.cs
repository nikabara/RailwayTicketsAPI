using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class VagonRepository : IVagonRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public VagonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<int?> AddVagon(Vagon vagon)
    {
        var result = default(int?);

        await _dbContext.AddAsync(vagon);
        await _dbContext.SaveChangesAsync();

        result = vagon.VagonId;

        return result;
    }

    public async Task<Vagon?> GetVagonByID(int id)
    {
        var result = default(Vagon);

        result = await _dbContext.Vagons.FirstOrDefaultAsync(v => v.VagonId == id);

        return result;
    }

    public async Task<bool> RemoveVagon(int id)
    {
        var result = default(bool);

        var targetVagon = await _dbContext.Vagons.FirstOrDefaultAsync(t => t.VagonId == id);

        if (targetVagon == null)
        {
            result = false;
        }
        else
        {
            _dbContext.Vagons.Remove(targetVagon);

            int rowsAffected = await _dbContext.SaveChangesAsync();

            result = rowsAffected > 0;
        }

        return result;
    }

    public async Task<bool> UpdateVagon(Vagon vagon)
    {
        var result = default(bool);

        var targetVagon = await _dbContext.Vagons
            .FirstOrDefaultAsync(t => t.VagonId == vagon.VagonId);

        if (targetVagon != null)
        {
            targetVagon.TrainId = 
                vagon.TrainId ?? targetVagon.TrainId;

            targetVagon.VagonType = vagon.VagonType;

            result = true;

            await _dbContext.SaveChangesAsync();
        }

        return result;
    }
    #endregion
}
