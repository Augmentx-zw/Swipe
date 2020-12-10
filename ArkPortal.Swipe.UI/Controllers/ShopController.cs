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
    public class ShopController : Controller
    {
        private readonly IHttpClientService _client;
        public ShopController(IHttpClientService client)
        {
            _client = client;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerShop customerShop, PrivateKey pkey)
        {
            customerShop.ShopId = Guid.NewGuid();
            customerShop.AccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _client.PostRequest(customerShop, "shop/Addshop");
            pkey.ShopId = customerShop.ShopId;
           var status = await _client.PostRequest(pkey, "Private/Addkey");
            result.EnsureSuccessStatusCode();
            status.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("create", "bank");
            }
            return View(customerShop);
        }

        public async Task<IActionResult> Details()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            if (shop == null)
            {
                return RedirectToAction("create", "shop");
            }
            var result = await _client.GetRequest(new CustomerShop(), $"shop/getshop?shop={shop.ShopId}");
            return View(result);
        }

        public async Task<IActionResult> Update(CustomerShop customerShop)
        {
            var result = await _client.PostRequest(customerShop, "shop/UpdateShop");
            var check = ValidationResponseCheck.IsValidResponse(result);
            if (!check.Error)
            {
                return RedirectToAction("details", "shop");
            }
            else
                return View(customerShop);
        }
    }
}