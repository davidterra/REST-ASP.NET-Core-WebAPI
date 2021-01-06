using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace Dummy.Integration.Tests
{
    public abstract class TestFixtureBase
        : IClassFixture<WebApplicationFactory<Dummy.Api.Startup>>, IDisposable
    {
        private bool disposedValue;

        protected  HttpClient Client { get; }

        protected TestFixtureBase(WebApplicationFactory<Dummy.Api.Startup> factory) => Client = factory.CreateClient();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Client.Dispose();
                }
                disposedValue = true;
            }
        }
        

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
