using Application.DTOs.CreditCardDTOs;
using Domain.Common;
using Domain.Entities;

namespace Application.Services.EntityServices.Abstractions;

public interface ICreditCardService
{
    public Task<ServiceResponse<int?>> AddCreditCard(AddCreditCardDTO creditCardDTO);
    public Task<ServiceResponse<GetCreditCardDTO>> GetCreditCardByID(int id);
    public Task<ServiceResponse<bool>> RemoveCreditCard(int id);
}
