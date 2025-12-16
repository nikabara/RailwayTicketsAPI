using Application.Abstractions;
using Application.DTOs.TransactionDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;


namespace Application.Services.EntityServices.Implementations;

public class TransactionService : ITransactionService
{
    #region Propertis
    private readonly ITransactionRepository _transactionRepository;
    #endregion

    #region Constructors
    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    #endregion

    #region Methods
    public async Task<ServiceResponse<List<GetTransactionDTO>>> GetTransactionsByUserId(int userId)
    {
        var response = new ServiceResponse<List<GetTransactionDTO>>();

        var userTransactions = (await _transactionRepository.GetTransactionsByuserId(userId))
            .Select(ut => new GetTransactionDTO
            {
                TransactionId = ut.TransactionId,
                TransactionState = Enum.GetName(typeof(TransactionStateType), ut.TransactionStateId)!,
                SeatId = ut.SeatId,
                TrainScheduleId = ut.TrainScheduleId,
                CreditCardId = ut.CreditCardId,
                CurrencyId = ut.CurrencyId,
                TransactionAmount = ut.TransactionAmount,
                TransactionDate = ut.TransactionDate,
                IsActive = ut.IsActive,
                Seat = new DTOs.SeatDTOs.GetSeatDTO
                {
                    SeatId = ut.Seat.SeatId,
                    VagonId = (int)ut.Seat.VagonId!,
                    SeatNumber = ut.Seat.SeatNumber,
                    SeatPrice = ut.Seat.SeatPrice,
                    SeatStatusId = ut.Seat.SeatStatusId
                },
                TrainSchedule = new DTOs.TrainScheduleDTOs.GetTrainScheduleDTO
                {
                    TrainScheduleId = ut.TrainSchedule.TrainScheduleId,
                    TrainId = ut.TrainSchedule.TrainId,
                    DepartureFrom = ut.TrainSchedule.DepartureFrom,
                    ArrivalAt = ut.TrainSchedule.ArrivalAt,
                    DepartureDate = ut.TrainSchedule.ArrivalDate,
                    ArrivalDate = ut.TrainSchedule.ArrivalDate
                },
                CreditCard = new DTOs.CreditCardDTOs.GetCreditCardDTO
                {
                    CreditCardId = ut.CreditCard.CreditCardId,
                    CreditCardNumber = ut.CreditCard.CreditCardNumber,
                    CreditCardIssuerId = ut.CreditCard.CreditCardIssuerId,
                    ExpirationDate = ut.CreditCard.ExpirationDate,
                    CVV = ut.CreditCard.CVV
                }
            }).ToList();

        if (userTransactions == null)
        {
            response.ErrorMessage = "No transactions found for the specified user.";
            response.IsSuccess = false;
        }
        else if (!userTransactions.Any())
        {
            response.ErrorMessage = "The user has no transactions.";
            response.IsSuccess = true;
            response.Data = userTransactions;
        }
        else
        {
            response.IsSuccess = true;
            response.Data = userTransactions;
        }

        return response;
    }
    #endregion
}
