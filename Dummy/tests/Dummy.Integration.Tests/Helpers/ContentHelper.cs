using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Dummy.Integration.Tests
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

        public static StringContent GetStringContent(string playload)
            => new StringContent(playload, Encoding.UTF8, "application/json");
    }
}
