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

    public async Task<List<TrainSchedule>> FilterTrainSchedules(TrainSchedule trainSchedule, string trainName, int? trainNumber)
    {
        // 1. Start with the base IQueryable
        IQueryable<TrainSchedule> query = _dbContext.TrainSchedule.Include(t => t.Train);

        // 2. Chain Where clauses based on non-null/non-default filter properties

        // Filter by TrainId (assuming 0 is the default/non-filter value)
        if (trainSchedule.TrainId > 0)
        {
            query = query.Where(ts => ts.TrainId == trainSchedule.TrainId);
        }

        // Filter by Destination (assuming null or empty string means no filter)
        if (!string.IsNullOrEmpty(trainSchedule.DepartureFrom))
        {
            // Using Contains for partial match or Equals for exact match
            query = query.Where(ts => ts.DepartureFrom.Contains(trainSchedule.DepartureFrom));
        }

        if (!string.IsNullOrEmpty(trainSchedule.ArrivalAt))
        {
            // Using Contains for partial match or Equals for exact match
            query = query.Where(ts => ts.ArrivalAt.Contains(trainSchedule.ArrivalAt));
        }

        // Filter by Departure Time (assuming default DateTime means no filter)
        // Note: You should handle DateTime comparisons carefully (e.g., filtering by date only)
        if (trainSchedule.DepartureDate != default(DateTime) && trainSchedule.DepartureDate != null)
        {
            // Example: Filter schedules that depart on the specified date
            query = query.Where(ts => ts.DepartureDate.Value.Date == trainSchedule.DepartureDate.Value.Date);
        }

        if (trainSchedule.ArrivalDate != default(DateTime) && trainSchedule.ArrivalDate != null)
        {
            // Example: Filter schedules that depart on the specified date
            query = query.Where(ts => ts.ArrivalDate.Value.Date == trainSchedule.ArrivalDate.Value.Date);
        }

        if (!string.IsNullOrWhiteSpace(trainName))
        {
            query = query.Where(ts => ts.Train.TrainName == trainName);
        }

        if (trainNumber != null)
        {
            query = query.Where(ts => ts.Train.TrainNumber == trainNumber);
        }

        // 3. Execute the query and return the results
        // ToListAsync() executes the SQL query against the database.
        return await query.ToListAsync();
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
