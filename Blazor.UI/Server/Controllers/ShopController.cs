using Blazor.UI.Server.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IHttpClientService _client;
        public ShopController(IHttpClientService client)
        {
            _client = client;
        }
        public async Task<IActionResult> Create(CustomerShop customerShop)
        {

            var result = await _client.PostRequest(customerShop, "shop/Addshop");
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                return Ok("Record Saved");
            }
            return Ok(customerShop);
        }

        public async Task<IActionResult> Details(Guid Shop)
        {
            var result = await _client.GetRequest(new CustomerBank(), $"shop/getshop?shop={Shop}");
            return Ok(result);
        }

        public async Task<IActionResult> Update(CustomerShop customerShop)
        {
            var result = await _client.PostRequest(customerShop, "bank/UpdateShop");
            var check = ValidationResponseCheck.IsValidResponse(result);
            if (!check.Error)
            {
                return RedirectToAction("Record has successfully been updated");
            }
            else
                return Ok("Failed to update record");
        }
    }
}