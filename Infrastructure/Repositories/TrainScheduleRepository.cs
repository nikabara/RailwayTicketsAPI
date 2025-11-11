using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories;

public class TrainScheduleRepository : ITrainScheduleRepository
{
    #region Properties
    private readonly ApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public TrainScheduleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<int?> AddTrainSchedule(TrainSchedule trainSchedule)
    {
        var result = default(int?);

        await _dbContext.TrainSchedule.AddAsync(trainSchedule);
        await _dbContext.SaveChangesAsync();

        result = trainSchedule.TrainScheduleId;

        return result;
    }

    public async Task<TrainSchedule?> GetTrainScheduleByID(int id)
    {
        var result = new TrainSchedule();

        result = await _dbContext.TrainSchedule
            .Include(t => t.Train)
            .FirstOrDefaultAsync(t => t.TrainScheduleId == id);

        return result;
    }

    public async Task<bool> RemoveTrainSchedule(int id)
    {
        var result = default(bool);

        var trainScheduleToDelete = await _dbContext.TrainSchedule.FirstOrDefaultAsync(t => t.TrainScheduleId == id);

        if (trainScheduleToDelete == null)
        {
            result = false;
        }
        else
        {
            _dbContext.TrainSchedule.Remove(trainScheduleToDelete);

            int rowsAffected = await _dbContext.SaveChangesAsync();

            result = rowsAffected > 0;
        }

        return result;
    }

    public async Task<bool> UpdateTrainSchedule(TrainSchedule trainSchedule)
    {
        var result = default(bool);

        var targetTrainSchedule = await _dbContext.TrainSchedule
            .FirstOrDefaultAsync(ts => ts.TrainScheduleId == trainSchedule.TrainScheduleId);

        if (targetTrainSchedule != null)
        {
            targetTrainSchedule.TrainId = 
                trainSchedule.TrainId ?? targetTrainSchedule.TrainId;

            targetTrainSchedule.DepartureFrom = trainSchedule.DepartureFrom == string.Empty
                ? targetTrainSchedule.DepartureFrom
                : trainSchedule.DepartureFrom;

            targetTrainSchedule.ArrivalAt = trainSchedule.ArrivalAt == string.Empty
                ? targetTrainSchedule.ArrivalAt 
                : trainSchedule.ArrivalAt;

            targetTrainSchedule.DepartureDate =
                trainSchedule.DepartureDate ?? targetTrainSchedule.DepartureDate;

            targetTrainSchedule.ArrivalDate =
                trainSchedule.ArrivalDate ?? targetTrainSchedule.ArrivalDate;

            result = true;

            await _dbContext.SaveChangesAsync();
        }

        return result;
    }
    #endregion
}
