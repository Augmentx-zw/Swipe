using Blazor.UI.Server.Services;
using Blazor.UI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IHttpClientService _client;

        public BankController(IHttpClientService client)
        {
            _client = client;
        }


        public async Task<IActionResult> Create(CustomerBank customerBank)
        {
            var result = await _client.PostRequest(customerBank, "bank/Addbank");
            result.EnsureSuccessStatusCode();
            if (result.IsSuccessStatusCode)
            {
                return Ok("Record Saved");
            }
            return Ok(customerBank);

        }

        public async Task<IActionResult> BankDetails(Guid shop)
        {
            var result = await _client.GetRequest(new CustomerBank(), $"bank/getbank?shop={shop}");
            return Ok(result);
        }

        public async Task<IActionResult> UpdateBank(CustomerBank customerBank)
        {

            var result = await _client.PostRequest(customerBank, "bank/updateBankDetails");
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