using ArkPortal.Swipe.UI.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArkPortal.Swipe.UI.Controllers
{
    [Authorize]
    public class SwipeController : Controller
    {
        private readonly IHttpClientService _client;

        public SwipeController(IHttpClientService client)
        {
            _client = client;
        }
        public async Task<ActionResult> Index()
        {
            var id  = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            if (shop == null)
            {
                return RedirectToAction("create", "shop");
            }
            List<PaymentDetail> result = await _client.GetRequest(new List<PaymentDetail>(), $"Payment/GetAllPaymnets?shopId={shop.ShopId}");
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Stats()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            List<PaymentDetail> result = await _client.GetRequest(new List<PaymentDetail>(), $"Payment/GetAllPaymnets?shopId={shop.ShopId}");

            double all = result.Count();
            double paid = result.Where(p => p.PaymentStatus == "Paid").Count();
            double Perfomance = (paid / all) * 100;

            ViewBag.Perfomance = Perfomance;
            ViewBag.Transactions = result.Count();
            ViewBag.Paid = result.Where(p => p.PaymentStatus == "Paid").Count();
            return View();
        }

        public async Task<IActionResult> TopReports()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            List<PaymentDetail> result = await _client.GetRequest(new List<PaymentDetail>(), $"Payment/GetAllPaymnets?shopId={shop.ShopId}");
            return View(result.Take(5).OrderByDescending(p=> p.CreatedOn).ToList());
        }

        public async Task<IActionResult> Reports()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            if (shop == null)
            {
                return RedirectToAction("create", "shop");
            }
            ViewBag.ShopId = shop.ShopId;
            List<PaymentDetail> result = await _client.GetRequest(new List<PaymentDetail>(), $"Payment/GetAllPaymnets?shopId={shop.ShopId}");
            return View(result.OrderByDescending(p => p.CreatedOn).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Keys()
        {
            var id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var shop = await _client.GetRequest(new CustomerShop(), $"shop/GetShopById?account={id}");
            if (shop == null)
            {
                return RedirectToAction("create", "shop");
            }
            ViewBag.ShopName = shop.ShopName;
            var result = await _client.GetRequest(new PrivateKey(), $"Private/GetKey?shop={shop.ShopId}");
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateKeys(PrivateKey privateKey)
        {
            var result = await _client.PostRequest(privateKey, "private/UpdateKey");
            var check = ValidationResponseCheck.IsValidResponse(result);
            if (!check.Error)
            {
                return RedirectToAction("keys", "swipe");
            }
            else
                return View(privateKey);
        }

    }
}
