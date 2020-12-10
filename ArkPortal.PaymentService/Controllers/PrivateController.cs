using ArkPortal.Domain.QueryHandler.Security;
using ArkPotal.Domain.CommandHandler.Security;
using ArkPotal.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArkPortal.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrivateController : ControllerBase
    {
        private readonly Mediator _mediator;
        public PrivateController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddKey")]
        public IActionResult Create([FromBody] AddKeyCommand command)
        {
            try
            {
                _mediator.Dispatch(command);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = true, ex.Message });
            }
        }

        [HttpGet("GetKey")]
        public IActionResult Details(Guid shop)
        {
            var result = _mediator.Dispatch(new GetKeyByShopIdQuery { ShopId = shop });
            return Ok(result);
        }

        [HttpPost("UpdateKey")]
        public IActionResult Update(UpDateKeyCommand command)
        {
            try
            {
                command.Key = Guid.NewGuid();
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