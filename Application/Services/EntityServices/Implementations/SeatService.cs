using Application.Abstractions;
using Application.BusinessLogics.SeatBookingBusinessLogic;
using Application.DTOs.SeatDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;

namespace Application.Services.EntityServices.Implementations;

public class SeatService : ISeatService
{
    #region Properties
    private readonly ISeatRepository _seatRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITrainScheduleRepository _trainScheduleRepository;
    private readonly IUserService _userService;
    #endregion

    #region Constructors
    public SeatService(ISeatRepository seatRepository, ITrainScheduleRepository trainScheduleRepository, ITransactionRepository transactionRepository, ITicketRepository ticketRepository, IUserRepository userRepository, IUserService userService)
    {
        _seatRepository = seatRepository;
        _trainScheduleRepository = trainScheduleRepository;
        _transactionRepository = transactionRepository;
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
        _userService = userService;
    }

    #endregion

    #region Methods
    public async Task<ServiceResponse<bool>> BookSeat(BookSeatDTO bookSeatDTO)
    {
        var response = new ServiceResponse<bool>();

        var bookSeatBusinessLogic = new BookSeatBusinessLogic
        (
            _seatRepository,
            _userRepository,
            _userService,
            _ticketRepository,
            _transactionRepository,
            _trainScheduleRepository,
            bookSeatDTO.UserId,
            bookSeatDTO.SeatId,
            bookSeatDTO.TransactionAmount,
            bookSeatDTO.TrainScheduleId,
            bookSeatDTO.CreditCardId,
            bookSeatDTO.CurrencyId
        );

        var bookSeatBLResponse = await bookSeatBusinessLogic.Execute();

        if (bookSeatBLResponse.IsError)
        {
            response.ErrorMessage = bookSeatBLResponse.ErrorMessage!;
            response.Data = false;
        }
        else
        {
            response.Data = true;
        }

        return response;
    }

    #endregion
}
