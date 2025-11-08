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
    #endregion

    #region Constructors
    public CreditCardService(ICreditCardRepository creditCardRepository, ICreditCardIssuerRepository creditCardIssuerRepository)
    {
        _creditCardRepository = creditCardRepository;
        _creditCardIssuerRepository = creditCardIssuerRepository;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<int?>> AddCreditCard(AddCreditCardDTO creditCardDTO)
    {
        var response = new ServiceResponse<int?>();

        var targetCreditCardIssuer = await _creditCardIssuerRepository.GetCreditCardIssuer(creditCardDTO.CreditCardIssuerId);

        if (targetCreditCardIssuer == null)
        {
            response.ErrorMessage = "Credit Card Issuer returned as null, cannot add credit card without a valid issuer";
            response.IsSuccess = false;
        }
        else
        {
            var creditCard = new CreditCard
            {
                CreditCardIssuerId = creditCardDTO.CreditCardIssuerId,
                ExpirationDate = creditCardDTO.ExpirationDate,
                CVV = creditCardDTO.CVV,
                CreditCardIssuer = targetCreditCardIssuer
            };

            int? addedCreditCardId = await _creditCardRepository.AddCreditCard(creditCard);

            if (addedCreditCardId != null && addedCreditCardId > 0)
            {
                response.IsSuccess = true;
                response.Data = addedCreditCardId;
            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage = "Error adding credit card";
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
