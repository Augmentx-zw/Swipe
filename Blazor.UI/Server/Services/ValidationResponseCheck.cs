using Blazor.UI.Shared;
using Newtonsoft.Json;
using System.Net.Http;

namespace Blazor.UI.Server.Services
{
    public class ValidationResponseCheck
    {
        public static ErrorCheck IsValidResponse(HttpResponseMessage response)
        {
            var isError = false;
            var message = "";
            if (response.IsSuccessStatusCode)
            {
                var content = JsonConvert.DeserializeObject<ErrorCheck>(response.Content.ReadAsStringAsync().Result);
                if (content != null && content.Error)
                {
                    message = content.Message;
                    isError = true;
                }
                else
                {
                    message = "Record has successfully been added.";
                }
            }
            return new ErrorCheck { Error = isError, Message = message };
        }
    }
}
