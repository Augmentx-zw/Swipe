using ArkPortal.Swipe.UI.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArkPortal.Swipe.UI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class PaymentController : Controller
    {
        private readonly IHttpClientService _client;

        public PaymentController(IHttpClientService client)
        {
            _client = client;
        }

        public async Task<IActionResult> Payment(Guid payment)
        {
            PaymentDetail result = await _client.GetRequest(new PaymentDetail(), $"payment/GetPaymentById?payment={payment}");
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Payment(PaymentDetail paymentDetail)
        {
            HttpResponseMessage result = await _client.PostRequest(paymentDetail, "payment/UpdatePayment");
            ErrorCheck check = ValidationResponseCheck.IsValidResponse(result);
            if (!check.Error)
            {
                return RedirectToAction("PaymentSuccess", new { payment = paymentDetail.PaymentDetailId }); ;
            }
            else
            {
                return RedirectToAction("PaymentError", new { payment = paymentDetail.PaymentDetailId }); ;
            }
        }

        public async Task<IActionResult> PaymentSuccess(Guid payment)
        {
            PaymentDetail result = await _client.GetRequest(new PaymentDetail(), $"payment/GetPaymentById?payment={payment}");
            return View(result);
        }

        public async Task<IActionResult> PaymentError(Guid payment)
        {
            PaymentDetail result = await _client.GetRequest(new PaymentDetail(), $"payment/GetPaymentById?payment={payment}");
            return View(result);
        }


    }
}