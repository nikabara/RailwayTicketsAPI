using Application.DTOs.TrainScheduleDTOs;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface ITrainScheduleFilterService
{
    public Task<ServiceResponse<List<ScheduleFilterDTO>>> Filter(ScheduleFilterDTO filterOptions);
}
