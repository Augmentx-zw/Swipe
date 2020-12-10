using ArkPortal.Swipe.UI.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArkPortal.Swipe.UI.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private readonly IHttpClientService _client;

        public BankController(IHttpClientService client)
        {
            _client = client;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerBank customerBank)
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            customerBank.ShopId = shop.ShopId;
            var result = await _client.PostRequest(customerBank, "bank/Addbank");
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Swipe");
            }
            return View(customerBank);

        }
        [HttpGet]
        public async Task<IActionResult> BankDetails()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            var result = await _client.GetRequest(new CustomerBank(), $"bank/getbank?shop={shop.ShopId}");
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBank(CustomerBank customerBank)
        {
            
            var result = await _client.PostRequest(customerBank, "bank/updateBankDetails");
            var check = ValidationResponseCheck.IsValidResponse(result);
            if (!check.Error)
            {
                return RedirectToAction("bankdetails", "bank");
            }
            else
                return View(customerBank);
        }

    }
}