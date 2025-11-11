using Application.Abstractions;
using Application.DTOs.UserCreditCardDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Enums;

namespace Application.Services.EntityServices.Implementations;

public class UserCreditCardService : IUserCreditCardService
{
    #region Properties
    private readonly IUserCreditCardRepository _userCreditCardRepository;
    #endregion

    #region Constructors
    public UserCreditCardService(IUserCreditCardRepository userCreditCardRepository)
    {
        _userCreditCardRepository = userCreditCardRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<List<GetUserCreditCardsNonSensitiveDTO>>> GetUserCardsNonSensitive(int userId)
    {
        var response = new ServiceResponse<List<GetUserCreditCardsNonSensitiveDTO>>();

        var userCards = await _userCreditCardRepository.GetUserCreditCards(userId);

        if (userCards == null)
        {
            response.ErrorMessage = "No credit cards found for the user.";
            response.IsSuccess = false;
        }
        else
        {
            var nonSensitiveCardInfo = userCards.Select(card => new GetUserCreditCardsNonSensitiveDTO
            {
                CreditCardId = card.CreditCardId,
                UserId = card.UserId,
                CreditCardIssuerId = card.CreditCardIssuerId,
                CreditCardIssuerName = Enum.GetName(typeof(CardIssuer), card.CreditCardIssuerId)!,
                LastFourDigits = card.CreditCardNumber[^4..]
            }).ToList();

            response.Data = nonSensitiveCardInfo;
            response.IsSuccess = true;
        }

        return response;
    }

    public async Task<ServiceResponse<List<GetUserCreditCardsSensitiveDTO>>> GetUserCardsSensitive(int userId)
    {
        var response = new ServiceResponse<List<GetUserCreditCardsSensitiveDTO>>();
        
        var userCards = await _userCreditCardRepository.GetUserCreditCards(userId);

        if (userCards == null)
        {
            response.ErrorMessage = "No credit cards found for the user.";
            response.IsSuccess = false;
        }
        else
        {
            var sensitiveCardInfo = userCards.Select(card => new GetUserCreditCardsSensitiveDTO
            {
                CreditCardId = card.CreditCardId,
                UserId = card.UserId,
                CreditCardIssuerId = card.CreditCardIssuerId,
                CreditCardIssuerName = Enum.GetName(typeof(CardIssuer), card.CreditCardIssuerId)!,
                ExpirationDate = card.ExpirationDate,
                CreditCardNumber = card.CreditCardNumber,
                CVV = card.CVV
            }).ToList();

            response.Data = sensitiveCardInfo;
            response.IsSuccess = true;
        }

        return response;
    }

    #endregion
}
