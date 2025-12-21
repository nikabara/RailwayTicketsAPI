using Application.Abstractions;
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
        var result = default(bool);

        var trainToDelete = await _dbContext.Trains.FirstOrDefaultAsync(t => t.TrainId == id);

        if (trainToDelete == null)
        {
            result = false;
        }
        else
        {
            _dbContext.Trains.Remove(trainToDelete);

            int rowsAffected = await _dbContext.SaveChangesAsync();

            result = rowsAffected > 0;
        }

        return result;
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

        result = await _dbContext.Trains.Include(v => v.Vagons).FirstOrDefaultAsync(t => t.TrainId == id);

        return result;
    }

    public async Task<bool> UpdateTrain(Train train)
    {
        var result = default(bool);

        var targetTrain = await _dbContext.Trains
            .FirstOrDefaultAsync(t => t.TrainId == train.TrainId);

        if (targetTrain != null)
        {
            targetTrain.TrainName = train.TrainName == string.Empty
                ? targetTrain.TrainName
                : train.TrainName;

            targetTrain.TrainNumber = train.TrainNumber ?? targetTrain.TrainNumber;

            result = true;

            await _dbContext.SaveChangesAsync();
        }

        return result;
    }

    public async Task<List<Train>> GetTrainsByValue(Train train)
    {
        IQueryable<Train> query = _dbContext.Trains;

        if (!string.IsNullOrWhiteSpace(train.TrainName))
        {
            query = query.Where(t => t.TrainName!.Contains(train.TrainName!));
        }

        if (train.TrainNumber != null)
        {
            query = query.Where(t => t.TrainNumber == train.TrainNumber);
        }

        return await query.ToListAsync();
    }
    #endregion
}
