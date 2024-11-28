using AppData.Authentication;
using Blazored.SessionStorage;
using BlazorServer.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;

namespace BlazorServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        //private readonly IHttpClientFactory _factory;
        private readonly HttpClient _httpClient;
        private string? _jwtCache;

        private static LoginResponse login;

        public event Action<string?>? LoginChange;

        public AuthenticationService(/*IHttpClientFactory factory,*/  HttpClient httpClient)
        {
            //_factory = factory;
            _httpClient = httpClient;
        }


        public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = login.JwtToken;
            return _jwtCache;
        }

        public async Task LogoutAsync()
        {
            string RequestURL = "https://localhost:7143/api/Authentication";
            var response = await _httpClient.DeleteAsync(RequestURL);

            login = null;

            _jwtCache = null;

            LoginChange?.Invoke(null);
        }

        private static string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);
            return jwt.Claims.First(c => c.Type == ClaimTypes.Name).Value;
        }


        public async Task<DateTime> LoginAsync(LoginModel model)
        {
            //var response = await _factory.CreateClient("ServerApi").PostAsync("api/authentication/login", JsonContent.Create(model));
            string RequestURL = "https://localhost:7143/api/Authentication/Login";
            var response = await _httpClient.PostAsJsonAsync(RequestURL,model);

            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Đăng nhập thất bại!");

            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (content == null) throw new InvalidDataException();

            _jwtCache = content.JwtToken;

            login = content;

            LoginChange?.Invoke(GetUsername(content.JwtToken));

            return content.Expiration;
        }
        public async Task<bool> RefreshAsync()
        {
            var model = new RefreshModel
            {
                AccessToken = login.JwtToken,
                RefreshToken = login.RefreshToken
            };
            string RequestURL = "https://localhost:7143/api/Authentication/Refresh";
            var response = await _httpClient.PostAsJsonAsync(RequestURL, model);
            if (!response.IsSuccessStatusCode)
            {
                await LogoutAsync();
                return false;
            }

            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (content == null) throw new InvalidDataException();

            login = content;

            _jwtCache = content.JwtToken;

            return true;
        }
    }
}
