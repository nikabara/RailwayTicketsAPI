using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TrainRepository : ITrainRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public TrainRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion

    #region Methods
    public async Task<bool> RemoveTrain(int id)
    {
        var trainToDelete = await _dbContext.Trains.FirstOrDefaultAsync(t => t.TrainId == id);

        if (trainToDelete == null)
        {
            return false;
        }

        _dbContext.Trains.Remove(trainToDelete);

        int affectedRowCount = await _dbContext.SaveChangesAsync();

        return affectedRowCount > 0;
    }

    public async Task<int?> AddTrain(Train train)
    {
        var result = default(int);

        await _dbContext.Trains.AddAsync(train);
        await _dbContext.SaveChangesAsync();

        result = train.TrainId;

        return result;
    }


    public async Task<Train?> GetTrainByID(int id)
    {
        var result = new Train();

        result = await _dbContext.Trains.FirstOrDefaultAsync(t => t.TrainId == id);

        return result;
    }


    #endregion
}
