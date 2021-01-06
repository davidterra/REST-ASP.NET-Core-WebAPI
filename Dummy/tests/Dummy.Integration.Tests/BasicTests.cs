using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Dummy.Integration.Tests
{
    public class BasicTests
        :IClassFixture<WebApplicationFactory<Dummy.Api.Startup>>
    {
        private readonly WebApplicationFactory<Dummy.Api.Startup> _factory;

        public BasicTests(WebApplicationFactory<Dummy.Api.Startup> factory) => _factory = factory;

        [Fact]
        public async Task Deve_consultar_a_saude_da_api()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/hc");

            // Assert
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            Assert.Contains("Healthy", content);
        }

    }
}
