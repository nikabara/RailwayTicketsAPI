using Application.Abstractions;
using Application.Services.EntityServices.Abstractions;
using Domain.Entities;
using Domain.Enums;

namespace Application.BusinessLogics.SeatBookingBusinessLogic;

public class BookSeatBusinessLogic
{
    #region Properties
    private readonly ISeatRepository _seatRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITrainScheduleRepository _trainScheduleRepository;

    private readonly IUserService _userService;

    private decimal transactionAmount;
    private int userId;
    private int seatId;
    private int trainScheduleId;
    private int creditCardId;
    private int currencyId;

    private BookSeatResult _result = new();
    private User? targetUser;
    private Seat? targetSeat;
    #endregion

    #region Constructor
    public BookSeatBusinessLogic(ISeatRepository seatRepository, IUserRepository userRepository, IUserService userService, ITicketRepository ticketRepository, ITransactionRepository transactionRepository, ITrainScheduleRepository trainScheduleRepository, int userId, int seatId, decimal transactionAmount, int trainScheduleId, int creditCardId, int currencyId)
    {
        this.userId = userId;
        this.seatId = seatId;
        this.transactionAmount = transactionAmount;
        this.trainScheduleId = trainScheduleId;
        this.creditCardId = creditCardId;
        this.currencyId = currencyId;
        _seatRepository = seatRepository;
        _userRepository = userRepository;
        _userService = userService;
        _ticketRepository = ticketRepository;
        _transactionRepository = transactionRepository;
        _trainScheduleRepository = trainScheduleRepository;
    }
    #endregion

    #region Methods
    public async Task<BookSeatResult> Execute()
    {
        await InitBusinessLogicProperties();

        if (!_result.IsError)
        {
            await AddTransactionToDB();
        }

        return _result;
    }

    private async Task AddTransactionToDB()
    {
        var targetTrainSchedule = await _trainScheduleRepository.GetTrainScheduleByID(trainScheduleId);

        var transaction = new Transaction
        {
            TransactionStateId = (int)TransactionStateType.Successful,
            UserId = userId,
            SeatId = seatId,
            TrainScheduleId = trainScheduleId,
            CreditCardId = creditCardId,
            CurrencyId = currencyId,
            TransactionDate = DateTime.Now,
            IsActive = true,
            TransactionAmount = transactionAmount,
            TransactionState = new TransactionState
            {
                TransactionStateId = (int)TransactionStateType.Successful,
                TransactionStateName = TransactionStateType.Successful.ToString()
            },
            Currency = new Currency
            {
                CurrencyId = currencyId,
                CurrencyName = Enum.GetName(typeof(CurrencyType), currencyId)!
            },
            TrainSchedule = targetTrainSchedule!,
            User = targetUser!,
            Seat = targetSeat!,
            CreditCard = targetUser!.CreditCards.FirstOrDefault(cc => cc.CreditCardId == creditCardId)!
        };

        var addedTransactionId = await _transactionRepository.AddTransaction(transaction);

        if (addedTransactionId == 0)
        {
            _result.IsError = true;
            _result.ErrorMessage = "Error creating transaction";
        }
        else
        {
            _result.IsError = false;
        }
    }

    private async Task InitBusinessLogicProperties()
    {
        targetUser = await _userRepository.GetUserByID(userId);

        if (targetUser != null && targetUser.UserBalance >= transactionAmount)
        {
            targetSeat = await _seatRepository.GetSeat(seatId);

            if (targetSeat != null)
            {
                await _seatRepository.UpdateSeat(new Seat
                {
                    SeatId = seatId,
                    SeatStatusId = (int)SeatStatuses.Reserved
                });

                var ticket = new Ticket
                {
                    UserId = userId,
                    SeatId = seatId,
                    DateOfBooking = DateTime.Now,
                    TicketPrice = targetSeat.SeatPrice,
                    User = targetUser,
                    TicketPaymentStatusId = (int)TicketPaymentStatus.Pending
                };

                var addedTicketId = await _ticketRepository.AddTicket(ticket);

                var userPaid = await _userService.DeductUserFunds(transactionAmount, userId);

                if (!userPaid.IsSuccess)
                {
                    _result.IsError = true;
                    _result.ErrorMessage = "Error processing payment";

                    await _seatRepository.UpdateSeat(new Seat
                    {
                        SeatId = seatId,
                        SeatStatusId = (int)SeatStatuses.Available
                    });
                }
                else if (addedTicketId == 0)
                {
                    _result.IsError = true;
                    _result.ErrorMessage = "Error creating ticket";

                    await _seatRepository.UpdateSeat(new Seat
                    {
                        SeatId = seatId,
                        SeatStatusId = (int)SeatStatuses.Available
                    });
                }
                else
                {
                    var isTicketUpdated = await _ticketRepository.UpdateTicket(new Ticket
                    {
                        TicketId = (int)addedTicketId,
                        TicketPaymentStatusId = (int)TicketPaymentStatus.Completed
                    });

                    var isSeatUpdated = await _seatRepository.UpdateSeat(new Seat
                    {
                        SeatId = seatId,
                        SeatStatusId = (int)SeatStatuses.Occupied
                    });

                    if (isTicketUpdated && isSeatUpdated)
                    {
                        _result.IsError = false;
                    }
                    else
                    {
                        _result.IsError = true;
                        _result.ErrorMessage = "Error updating ticket status";

                        await _userService.AddUserFunds(transactionAmount, userId);

                        await _ticketRepository.UpdateTicket(new Ticket
                        {
                            TicketId = (int)addedTicketId,
                            TicketPaymentStatusId = (int)TicketPaymentStatus.Canceled
                        });

                        await _seatRepository.UpdateSeat(new Seat
                        {
                            SeatId = seatId,
                            SeatStatusId = (int)SeatStatuses.Available
                        });
                    }
                }
            }
            else
            {
                _result.IsError = true;
                _result.ErrorMessage = $"Seat with ID {seatId} was not found";
            }
        }
        else
        {
            _result.IsError = true;
            _result.ErrorMessage = "Invalid user or not sufficient funds";
        }
    }

    #endregion

    #region Nested classes
    public class BookSeatResult
    {
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }
    }
    #endregion
}
