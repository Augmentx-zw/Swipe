using ArkPortal.Domain.QueryHandler.BankDetails;
using ArkPotal.Domain.CommandHandler.BankDetails;
using ArkPotal.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArkPortal.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly Mediator _mediator;
        public BankController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddBank")]
        public IActionResult Create([FromBody] AddBankDetailCommand command)
        {
            try
            {
                command.BankId = Guid.NewGuid();
                _mediator.Dispatch(command);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = true, ex.Message });
            }
        }

        [HttpGet("GetBank")]
        public IActionResult BankDetails(Guid shop)
        {
            var result = _mediator.Dispatch(new GetBankByShopIdQuery { ShopId = shop });
            return Ok(result);
        }

        [HttpPost("UpdateBankDetails")]
        public IActionResult UpdateBank(UpDateBankDetailCommand command)
        {
            try
            {
                _mediator.Dispatch(command);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return Ok(new { Error = true, ex.Message });
            }
        }

    }
}