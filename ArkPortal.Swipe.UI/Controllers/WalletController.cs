using ArkPortal.Swipe.UI.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArkPortal.Swipe.UI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class WalletController : Controller
    {
        private readonly IHttpClientService _client;

        public WalletController(IHttpClientService client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerWallet customerWallet)
        {
            var result = await _client.PostRequest(customerWallet, "wallet/AddBalance");
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                return View("Balances", new { Shop = customerWallet.ShopId });
            }
            return View(customerWallet);
        }

        public async Task<IActionResult> Balances()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            var result = await _client.GetRequest(new CustomerBank(), $"wallet/GetBalances?shop={shop.ShopId}");
            return View(result);
        }

        public async Task<IActionResult> UpdateWallet(CustomerWallet customerWallet)
        {
            var result = await _client.PostRequest(customerWallet, "wallet/UpdateBalance");
            var check = ValidationResponseCheck.IsValidResponse(result);
            if (!check.Error)
            {
                return View("Balances");
            }
            else
                return View(customerWallet);
        }
    }

}
