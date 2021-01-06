using System.Net.Http;
using System.Threading.Tasks;

namespace Dummy.Integration.Tests
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, object obj)
            => client.PostAsync(requestUri, ContentHelper.GetStringContent(obj));

        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string requestUri, string payload)
            => client.PostAsync(requestUri, ContentHelper.GetStringContent(payload));
    }
}
