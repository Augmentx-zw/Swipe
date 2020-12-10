using Blazor.UI.Server.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IHttpClientService _client;

        public PaymentController(IHttpClientService client)
        {
            _client = client;
        }

        [HttpPost("InitPayment")]
        public async Task<IActionResult> InitiatePayment(PaymentDetail paymentDetail)
        {
            try
            {
                paymentDetail.PaymentDetailId = Guid.NewGuid();
                var result = await _client.PostRequest(paymentDetail, "payment/InitPayment");
                var url = "http://augmentx-001-site13.itempurl.com/#/pay";
                var newUrl = $"{url}/{paymentDetail.PaymentDetailId}";
                return Ok(newUrl);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = true, ex.Message });
            }
        }

        public async Task<IActionResult> GetPaymentDetailId(Guid Payment)
        {
            var result = await _client.GetRequest(new CustomerBank(), $"payment/GetPaymentById?payment={Payment}");
            return Ok(result);
        }

        [HttpPost("UpdatePayment")]
        public async Task<IActionResult> UpdatePaymentDetails(PaymentDetail paymentDetail)
        {
            var result = await _client.PostRequest(paymentDetail, "payment/UpdatePayment");
            var check = ValidationResponseCheck.IsValidResponse(result);
            if (!check.Error)
            {
                return RedirectToAction("Record has successfully been updated");
            }
            else
                return Ok("Failed to process payment");
        }

        [HttpGet("GetAllPaymnets")]
        public async Task<IActionResult> GetAllPaymnets(Guid shopId)
        {
            var list = await _client.GetRequest(new List<PaymentDetail>(), $"/payment/GetAllPaymnets?shopId={shopId}");
            return Ok(list.OrderByDescending(a => a.CreatedOn));
        }

        public async Task<IActionResult> GetAllTotalPaymnets(Guid shopId)
        {
            var result = await _client.GetRequest(new CustomerBank(), $"payment/getAllTotalPaymnets?shop={shopId}");
            return Ok(result);
        }

        [HttpGet("GetAllPaidPaymnets")]
        public async Task<IActionResult> GetAllPaidPaymnets(Guid shopId)
        {
            var result = await _client.GetRequest(new CustomerBank(), $"payment/GetAllPaidPaymnets?shop={shopId}");
            return Ok(result);
        }

        [HttpGet("Getperfomance")]
        public async Task<IActionResult> Getperfomance(Guid shopId)
        {
            var result = await _client.GetRequest(new CustomerBank(), $"payment/Getperfomance?shop={shopId}");
            return Ok(result);
        }

    }
}