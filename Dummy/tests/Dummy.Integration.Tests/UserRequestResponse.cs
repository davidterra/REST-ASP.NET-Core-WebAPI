using System.Collections.Generic;

namespace Dummy.Integration.Tests
{
    public class Claim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<Claim> Claims { get; set; }
    }

    public class Data
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }

    public class UserRequestResponse
    {
        public bool Success { get; set; }
        public Data Data { get; set; }
    }
}


