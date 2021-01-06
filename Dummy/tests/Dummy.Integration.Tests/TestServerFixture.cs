using Dummy.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;

namespace Dummy.Integration.Tests
{
    public class TestServerFixture : IDisposable
	{
        private bool disposedValue;
        public HttpClient Client { get; } 
        public TestServer TestServer { get; }

        public TestServerFixture()
        {
            TestServer = new TestServer(new WebHostBuilder()
                                                .UseEnvironment("Test")
                                                .UseStartup<Startup>());
            Client = TestServer.CreateClient();
        }

        #region Disposing
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TestServer?.Dispose();
                    Client?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
