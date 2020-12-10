using ArkPortal.Domain.QueryHandler.Payments;
using ArkPortal.Domain.QueryHandler.Security;
using ArkPotal.Domain.CommandHandler.InitiatePayment;
using ArkPotal.PaymentService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ArkPortal.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly Mediator _mediator;
        public PaymentController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("InitPayment")]
        public IActionResult InitiatePayment([FromBody] AddInitiatePaymentCommand command)
        {
            try
            {
                command.PaymentDetailId = Guid.NewGuid();
                Guid privatekey = _mediator.Dispatch(new GetKeyByShopIdQuery { ShopId = command.ShopId }).Key;
                command.PrivateKey = privatekey.ToString();
                _mediator.Dispatch(command);
                var url = "http://augmentx-001-site7.itempurl.com/payment/payment?payment";
                var newUrl = $"{url}={command.PaymentDetailId}";
                return Ok(newUrl);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = true, ex.Message });
            }
        }

        [HttpGet("GetPaymentById")]
        public IActionResult GetPaymentDetailId(Guid Payment)
        {
            var result = _mediator.Dispatch(new GetPaymentByIdQuery { PaymentDetailId = Payment });
            return Ok(result);
        }

        [HttpPost("UpdatePayment")]
        public IActionResult UpdatePaymentDetails(UpDatePaymentCommand command)
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

        [HttpGet("GetAllPaymnets")]
        public IActionResult GetPaymnets(Guid shopId)
        {
            var result = _mediator.Dispatch(new GetAllPaymentsQueryByAccountId { ShopId = shopId });
            return Ok(result);
        }

        [HttpGet("GetAllTotalPaymnets")]
        public IActionResult GetAllTotalPaymnets(Guid shopId)
        {
            var result = _mediator.Dispatch(new GetAllPaymentsQueryByAccountId { ShopId = shopId }).Count();
            return Ok(result);
        }

        [HttpGet("GetAllPaidPaymnets")]
        public IActionResult GetAllPaidPaymnets(Guid shopId)
        {
            var result = _mediator.Dispatch(new GetAllPaymentsQueryByAccountId { ShopId = shopId }).Where(p => p.PaymentStatus == "Paid").Count();
            return Ok(result);
        }

        [HttpGet("Getperfomance")]
        public IActionResult Getperfomance(Guid shopId)
        {
            double all = _mediator.Dispatch(new GetAllPaymentsQueryByAccountId { ShopId = shopId }).Count();
            double paid = _mediator.Dispatch(new GetAllPaymentsQueryByAccountId { ShopId = shopId }).Where(p => p.PaymentStatus == "Paid").Count();
            double result = (paid / all) * 100;
            result = Math.Round(result, 2);
            return Ok(result);
        }

        [HttpPost("DeletePayment")]
        public IActionResult Delete(DeletePaymentCommand command)
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

        [HttpPost("PemDeletePayment")]
        public IActionResult PemDelete(PemDeletePaymentCommand command)
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