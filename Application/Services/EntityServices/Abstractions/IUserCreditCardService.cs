using Application.DTOs.UserCreditCardDTOs;
using Domain.Common;

namespace Application.Services.EntityServices.Abstractions;

public interface IUserCreditCardService
{
    public Task<ServiceResponse<List<GetUserCreditCardsNonSensitiveDTO>>> GetUserCardsNonSensitive(int userId);
    public Task<ServiceResponse<List<GetUserCreditCardsSensitiveDTO>>> GetUserCardsSensitive(int userId);
}
