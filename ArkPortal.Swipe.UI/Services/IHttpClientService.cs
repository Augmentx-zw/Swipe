using System.Net.Http;
using System.Threading.Tasks;

namespace ArkPortal.Swipe.UI.Services
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> PostRequest<T>(T command, string actionUrl) where T : class;
        Task<T> GetRequest<T>(T contectResult, string contentUrl) where T : class;
    }
}
