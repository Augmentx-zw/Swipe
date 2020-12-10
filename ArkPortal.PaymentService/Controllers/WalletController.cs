using ArkPortal.Domain.QueryHandler.WalletDetails;
using ArkPotal.Domain.CommandHandler.WalletDetails;
using ArkPotal.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArkPortal.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly Mediator _mediator;
        public WalletController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddBalance")]
        public IActionResult Create([FromBody] AddBalanceDetailCommand command)
        {
            try
            {
                command.BalanceId = Guid.NewGuid();
                _mediator.Dispatch(command);
                var result = _mediator.Dispatch(new GetBalanceByIdQuery { BalanceId = command.BalanceId });
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = true, ex.Message });
            }
        }

        [HttpGet("GetBalances")]
        public IActionResult Balances(Guid Shop)
        {
            var result = _mediator.Dispatch(new GetAllBalancesQueryByShopId { ShopId = Shop });
            return Ok(result);
        }

        [HttpPost("UpdateBalance")]
        public IActionResult UpdateBank(UpDateBalanceDetailCommand command)
        {
            try
            {
                _mediator.Dispatch(command);
                var result = _mediator.Dispatch(new GetBalanceByIdQuery { BalanceId = command.BalanceId });
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Ok(new { Error = true, ex.Message });
            }
        }

    }
}