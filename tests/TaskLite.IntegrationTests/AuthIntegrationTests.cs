using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskLite.Api.Controllers;
using Task = System.Threading.Tasks.Task;

namespace TaskLite.IntegrationTests
{
    public class AuthIntegrationTests : IClassFixture<TestWebAppFactory>
    {
        //private readonly HttpClient _client;

        //private const string Email = "jwtuser@example.com";
        //private const string Password = "Passw0rd!";
        //private const string FullName = "JWT Tester";

        //public AuthIntegrationTests(TestWebAppFactory factory) => _client = factory.CreateClient();

        //[Fact]
        //public async Task Register_And_Login_Should_Return_Valid_Jwt()
        //{
        //    // Register user
        //    RegisterRequest register = new(Email, Password, FullName);
        //    var regResponse = await _client.PostAsJsonAsync("/api/auth/register", register);
        //    Assert.Equal(HttpStatusCode.OK, regResponse.StatusCode);

        //    // Login
        //    LoginRequest login = new(Email, Password);
        //    var loginResponse = await _client.PostAsJsonAsync("/api/auth/login", login);
        //    Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        //    // Extract token
        //    var body = await loginResponse.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        //    Assert.True(body!.ContainsKey("token"));

        //    var token = body["token"];
        //    Assert.False(string.IsNullOrEmpty(token));

        //    // Access a protected endpoint without token
        //    var unauthResponse = await _client.GetAsync("/api/projects");
        //    Assert.Equal(HttpStatusCode.Unauthorized, unauthResponse.StatusCode);

        //    // Access same endpoint with token
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //    var authResponse = await _client.GetAsync("/api/projects");
        //    Assert.Equal(HttpStatusCode.OK, authResponse.StatusCode);
        //}
    }
}
