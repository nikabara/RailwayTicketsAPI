using Application.Abstractions;
using Application.BusinessLogics.SeatBookingBusinessLogic;
using Application.DTOs.SeatDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Domain.Entities;

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
            response.IsSuccess = true;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> RemoveSeat(int seatId)
    {
        var response = new ServiceResponse<bool>();

        var isTrainRemoved = await _seatRepository.RemoveSeat(seatId);

        if (isTrainRemoved)
        {
            response.Data = true;
            response.IsSuccess = true;
        }
        else
        {
            response.Data = false;
            response.IsSuccess = false;
        }

        return response;
    }

    public async Task<ServiceResponse<GetSeatDTO>> GetSeatById(int seatId)
    {
        var response = new ServiceResponse<GetSeatDTO>();

        var targetSeat = await _seatRepository.GetSeat(seatId);

        if (targetSeat == null)
        {
            response.ErrorMessage = "No seat found";
            response.IsSuccess = false;
        }
        else
        {
            var mappedSeat = new GetSeatDTO
            {
                SeatId = targetSeat.SeatId,
                VagonId = targetSeat.VagonId,
                SeatNumber = targetSeat.SeatNumber,
                SeatPrice = targetSeat.SeatPrice,
                SeatStatusId = targetSeat.SeatStatusId
            };

            response.Data = mappedSeat;
            response.IsSuccess = true;
        }

        return response;
    }

    public async Task<ServiceResponse<List<GetSeatDTO>>> GetSeatsByVagonID(int vagonId)
    {
        var response = new ServiceResponse<List<GetSeatDTO>>();

        var vagonSeats = await _seatRepository.GetSeatsByVagonID(vagonId);

        if (vagonSeats == null)
        {
            response.ErrorMessage = "No seats found for the specified vagon ID.";
            response.IsSuccess = false;
        }
        else
        {
            var seats = vagonSeats.Select(s => new GetSeatDTO
            {
                SeatId = s.SeatId,
                VagonId = (int)s.VagonId!,
                SeatNumber = s.SeatNumber,
                SeatPrice = s.SeatPrice,
                SeatStatusId = s.SeatStatusId
            }).ToList();

            response.Data = seats;
            response.IsSuccess = true;
        }

        return response;
    }

    public async Task<ServiceResponse<bool>> RemoveAllVagonSeats(int vagonId)
    {
        var response = new ServiceResponse<bool>();

        var vagonSeats = await _seatRepository.GetSeatsByVagonID(vagonId);

        if (vagonSeats == null)
        {
            response.ErrorMessage = "No seats found for the specified vagon ID.";
            response.IsSuccess = false;
        }
        else
        {
            foreach (var seat in vagonSeats)
            {
                var removalResult = await _seatRepository.RemoveSeat(seat.SeatId);

                if (!removalResult)
                {
                    response.ErrorMessage += $" Failed to remove seat with ID {seat.SeatId},";
                }
            }

            response.ErrorMessage = response.ErrorMessage.Trim([' ', ',']);

            response.IsSuccess = true;
            response.Data = true;
        }

        return response;
    }


    #endregion
}
