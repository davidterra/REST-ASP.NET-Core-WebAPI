using Dummy.Api.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dummy.Integration.Tests
{
    public static  class Auth
    {
        
        public static async Task<string> GetAccessTokenAsync(HttpClient client)
        {

            const string Request = "api/v1/entrar";

            var postResponse = await client.PostAsync(Request, Login());

            postResponse.EnsureSuccessStatusCode();

            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<UserRequestResponse>(jsonFromPostResponse);
            
            return response.Data.AccessToken;

            LoginUserViewModel Login() => new LoginUserViewModel { Email = "pac-man@test.it", Password = "Test@123" };
        }
        

    }
}