using Application.Abstractions;
using Application.DTOs.CreditCardDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Implementations;

public class CreditCardService : ICreditCardService
{
    #region Properties
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly ICreditCardIssuerRepository _creditCardIssuerRepository;
    private readonly IUserRepository _userRepository;
    #endregion

    #region Constructors
    public CreditCardService(ICreditCardRepository creditCardRepository, ICreditCardIssuerRepository creditCardIssuerRepository, IUserRepository userRepository)
    {
        _creditCardRepository = creditCardRepository;
        _creditCardIssuerRepository = creditCardIssuerRepository;
        _userRepository = userRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<int?>> AddCreditCard(AddCreditCardDTO creditCardDTO)
    {
        var response = new ServiceResponse<int?>();

        var targetCreditCardIssuer = await _creditCardIssuerRepository.GetCreditCardIssuer(creditCardDTO.CreditCardIssuerId);

        var targetUser = await _userRepository.GetUserByID(creditCardDTO.UserId);

        if (targetCreditCardIssuer == null)
        {
            response.ErrorMessage = "Credit Card Issuer returned as null, cannot add credit card without a valid issuer";
            response.IsSuccess = false;
        }
        else if (targetUser == null)
        {
            response.ErrorMessage = "User returned as null, cannot add credit card without a valid user";
            response.IsSuccess = false;
        }
        else
        {
            var targetCard = await _creditCardRepository
                .GetCreditCardByInfo(creditCardDTO.CreditCardNumber, creditCardDTO.CVV);

            CreditCard cardToProcess;
            int? finalCreditCardId = null;

            if (targetCard == null) // Card is New
            {
                cardToProcess = new CreditCard
                {
                    UserId = creditCardDTO.UserId,
                    CreditCardIssuerId = creditCardDTO.CreditCardIssuerId,
                    ExpirationDate = creditCardDTO.ExpirationDate,
                    CVV = creditCardDTO.CVV,
                    CreditCardNumber = creditCardDTO.CreditCardNumber,
                    CreditCardIssuer = targetCreditCardIssuer,
                    Users = new List<User> { targetUser }
                };

                finalCreditCardId = await _creditCardRepository.AddCreditCard(cardToProcess);

                if (finalCreditCardId.HasValue && finalCreditCardId > 0)
                {
                    response.IsSuccess = true;
                    response.Data = finalCreditCardId;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Error adding new credit card and link";
                }
            }
            else // Card Exists (Shared Card)
            {
                cardToProcess = targetCard;
                finalCreditCardId = cardToProcess.CreditCardId;

                if (cardToProcess.Users.Any(u => u.UserId == targetUser.UserId))
                {
                    response.IsSuccess = true;
                    response.Data = finalCreditCardId;
                    response.ErrorMessage = "Credit Card already linked to this user.";
                }
                else
                {
                    cardToProcess.Users.Add(targetUser);

                    int rowsAffected = await _creditCardRepository.UpdateCreditCard(cardToProcess);

                    if (rowsAffected > 0)
                    {
                        response.IsSuccess = true;
                        response.Data = finalCreditCardId;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Error establishing link for existing credit card.";
                    }
                }
            }
        }

        return response;
    }

    public async Task<ServiceResponse<GetCreditCardDTO>> GetCreditCardByID(int id)
    {
        var response = new ServiceResponse<GetCreditCardDTO>();

        var creditCard = await _creditCardRepository.GetCreditCardByID(id);

        if (creditCard == null)
        {
            response.ErrorMessage = "Credit Card returned as null";
            response.IsSuccess = false;
        }
        else
        {
            var creditCardResultDTO = new GetCreditCardDTO()
            {
                CreditCardId = creditCard.CreditCardId,
                CreditCardIssuerId = creditCard.CreditCardIssuerId,
                ExpirationDate = creditCard.ExpirationDate,
                CVV = creditCard.CVV,
                //CreditCardIssuer = creditCard.CreditCardIssuer
            };

            response.IsSuccess = true;
            response.Data = creditCardResultDTO;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> RemoveCreditCard(int id)
    {
        var response = new ServiceResponse<bool>();

        bool isCreditCardRemoved = await _creditCardRepository.RemoveCreditCard(id);

        if (isCreditCardRemoved)
        {
            response.IsSuccess = true;
            response.Data = true;
        }
        else
        {
            response.IsSuccess = false;
            response.ErrorMessage = $"Error removing credit card with an id : {id}";
        }

        return response;
    }

    #endregion
}
