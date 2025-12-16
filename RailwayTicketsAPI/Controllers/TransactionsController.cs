using Application.DTOs.TransactionDTO;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        #region Properties
        private readonly ITransactionService _transactionService;
        #endregion 

        #region Constructors
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        #endregion

        #region Methods
        [HttpGet("get-user-transactions/{userId:int}")]
        public async Task<ActionResult<ServiceResponse<List<GetTransactionDTO>>>> GetTransactionsByUserId(int userId)
{
            var response = await _transactionService.GetTransactionsByUserId(userId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        #endregion
    }
}
