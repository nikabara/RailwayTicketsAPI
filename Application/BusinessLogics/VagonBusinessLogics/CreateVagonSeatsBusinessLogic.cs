using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.ObjectPool;

namespace Application.BusinessLogics.VagonBusinessLogics;

public class CreateVagonSeatsBusinessLogic
{
    #region Properties
    private readonly ISeatRepository _seatRepository;
    private readonly IVagonRepository _vagonRepository;

    private readonly int _vagonId;
    private Vagon _vagon;

    private CreateVagonSeatsResult _result = new();
    #endregion

    #region Constructors
    public CreateVagonSeatsBusinessLogic(ISeatRepository seatRepository, IVagonRepository vagonRepository, int VagonId)
    {
        _seatRepository = seatRepository;
        _vagonRepository = vagonRepository;
        _vagonId = VagonId;
    }
    #endregion

    #region Methods
    public async Task<CreateVagonSeatsResult> Execute()
    {
        await InitBusinessLogicProperties();

        if (!_result.IsError)
        {
            await AddVagonSeatsToDB(_vagon.Capacity);
        }

        return _result;
    }

    public async Task InitBusinessLogicProperties()
    {
        var vagon = await _vagonRepository.GetVagonByID(_vagonId);

        if (vagon == null)
        {
            _result.IsError = true;
            _result.ErrorMessage = $"Vagon with ID {_vagonId} was not found";
        }
        else
        {
            _result.IsError = false;

            _vagon = vagon;
        }
    }
    
    public async Task AddVagonSeatsToDB(int? seatCount)
    {
        if (seatCount != null)
        {

            for (int i = 0; i < seatCount; i++)
            {
                int seatPrice = default;

                switch (_vagon.VagonType)
                {
                    case VagonType.FirstClass:
                        seatPrice = (int)SeatPrice.FirstClass;
                        break;
                    case VagonType.SecondClass:
                        seatPrice = (int)SeatPrice.SecondClass;
                        break;
                    case VagonType.BusinessClass:
                        seatPrice = (int)SeatPrice.BusinessClass;
                        break;
                    default:
                        break;
                }

                await _seatRepository.AddSeat(new Seat
                {
                    VagonId = _vagon.VagonId,
                    SeatNumber = getVagonSeatNumber(i),
                    SeatPrice = seatPrice,
                    IsOccupied = false,
                    Vagon = _vagon
                });
            }
        }
    }

    private string getVagonSeatNumber(int index)
    {
        char[] letters = ['A', 'B', 'C', 'D'];

        return $"{(index / 4) + 1}{letters[index % 4]}";
    }
    #endregion

    #region Nested classes
    public class CreateVagonSeatsResult
    {
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }
    }
    #endregion
}
