using Dummy.Api;
using Dummy.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace Dummy.Integration.Tests
{

    public abstract class UserFixture<TDbContext>
        : IClassFixture<CustomWebApplicationFactory<Startup, TDbContext>>
        where TDbContext : DbContext
    {
        protected TDbContext Db { get; }
    }

    [TestCaseOrderer("Dummy.Integration.Tests.PriorityOrderer", "Dummy.Integration.Tests")]
    public class UserInMemoryTests : UserFixture<Api.Data.ApplicationDbContext>
    //:  IClassFixture<CustomWebApplicationFactory<Startup, Api.Data.ApplicationDbContext>>
    {
        private readonly CustomWebApplicationFactory<Startup, Api.Data.ApplicationDbContext> _factory;

        public UserInMemoryTests(CustomWebApplicationFactory<Startup, Api.Data.ApplicationDbContext> factory) => _factory = factory;

        [Fact, TestPriority(1)]

        public async Task Deve_criar_um_usuario()
        {
            // Arrange

            var client = _factory.CreateClient();

            const string Request = "/api/v1/nova-conta";

            // Act

            var payload = new RegisterUserViewModel
            {
                Email = "pac-man@test.it",
                Password = "Test@123",
                ConfirmPassword = "Test@123",
            };

            var postResponse = await client.PostAsync(Request, payload);
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<UserRequestResponse>(jsonFromPostResponse);

            // Asset
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, postResponse.StatusCode);
            Assert.True(response.Success);
        }

        [Theory, TestPriority(2)]
        [InlineData("pac-man@test.it", "Test@123")]
        public async Task Deve_autenticar_um_usuario(string email, string password)
        {
            // Arrange
            var client = _factory.CreateClient();

            const string Request = "api/v1/entrar";

            var payload = new LoginUserViewModel { Email = email, Password = password };

            // Act
            var postResponse = await client.PostAsync(Request, payload);
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<UserRequestResponse>(jsonFromPostResponse);

            // Asset
            postResponse.EnsureSuccessStatusCode();
            Assert.True(response.Success);            

        }

       
    }
}
