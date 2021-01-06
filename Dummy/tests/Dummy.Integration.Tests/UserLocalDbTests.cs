using Dummy.Api.ViewModels;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xunit;

namespace Dummy.Integration.Tests
{
    public class UserLocalDbTests
        : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _serverFixture;

        public UserLocalDbTests(TestServerFixture serverFixture) => _serverFixture = serverFixture;

        [Fact]
        public async Task Deve_criar_um_usuario()
        {
            // Arrange

            const string Request = "/api/v1/nova-conta";

            // Act

            var payload = new RegisterUserViewModel
            {
                Email = "pac-man@test.it",
                Password = "Test@123",
                ConfirmPassword = "Test@123",
            };

            var postResponse = await _serverFixture.Client.PostAsync(Request, payload);
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<UserRequestResponse>(jsonFromPostResponse);

            // Asset
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, postResponse.StatusCode);
            Assert.True(response.Success);
        }


    }    
}


