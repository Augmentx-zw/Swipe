using ArkPortal.Domain.QueryHandler.Shop;
using ArkPotal.Domain.CommandHandler.Shop;
using ArkPotal.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArkPortal.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly Mediator _mediator;
        public ShopController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddShop")]
        public IActionResult Create([FromBody] AddShopCommand command)
        {
            try
            {
                _mediator.Dispatch(command);
                var result = _mediator.Dispatch(new GetShopByShopIdQuery { ShopId = command.ShopId });
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = true, ex.Message });
            }
        }

        [HttpGet("GetShop")]
        public IActionResult Details(Guid Shop)
        {
            var result = _mediator.Dispatch(new GetShopByShopIdQuery { ShopId = Shop });
            return Ok(result);
        }

        [HttpGet("GetShopById")]
        public IActionResult DetailsbyAccount(Guid account)
        {
            var result = _mediator.Dispatch(new GetShopByAccountIdQuery { AccountId = account });
            return Ok(result);
        }

        [HttpPost("UpdateShop")]
        public IActionResult Update(UpDateShopCommand command)
        {
            try
            {
                _mediator.Dispatch(command);
                var result = _mediator.Dispatch(new GetShopByShopIdQuery { ShopId = command.ShopId });
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return Ok(new { Error = true, ex.Message });
            }
        }
    }
}