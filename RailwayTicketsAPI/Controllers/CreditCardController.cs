using Application.DTOs.CreditCardDTOs;
using Application.Services.EntityServices.Abstractions;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RailwayTicketsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        #region Properties
        private readonly ICreditCardService _creditCardService;
        #endregion

        #region Constructors
        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }
        #endregion

        #region Methods
        [HttpGet("get-credit-card/{id:int}")]
        public async Task<ActionResult<ServiceResponse<int?>>> GetCreditCard(int id)
        {
            var response = await _creditCardService.GetCreditCardByID(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("add-credit-card")]
        public async Task<ActionResult<ServiceResponse<AddCreditCardDTO>>> AddCreditCard(AddCreditCardDTO creditCardDTO)
        {
            var response = await _creditCardService.AddCreditCard(creditCardDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete("remove-credit-card/{id:int}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RemoveCreditCard(int id)
        {
            var response = await _creditCardService.RemoveCreditCard(id);

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
