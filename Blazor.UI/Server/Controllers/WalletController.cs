using Blazor.UI.Server.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IHttpClientService _client;

        public WalletController(IHttpClientService client)
        {
            _client = client;
        }

        public async Task<IActionResult> Create(CustomerWallet customerWallet)
        {
            var result = await _client.PostRequest(customerWallet, "wallet/AddBalance");
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                return Ok("Record Saved");
            }
            return Ok(customerWallet);
        }

        public async Task<IActionResult> Balances(Guid Shop)
        {
            var result = await _client.GetRequest(new CustomerBank(), $"wallet/GetBalances?shop={Shop}");
            return Ok(result);
        }

        public async Task<IActionResult> UpdateWallet(CustomerWallet customerWallet)
        {
            var result = await _client.PostRequest(customerWallet, "wallet/UpdateBalance");
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
