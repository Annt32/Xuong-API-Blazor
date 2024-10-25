using AppData.Authentication;
using Blazored.SessionStorage;
using BlazorServer.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _factory;
        private ISessionStorageService _sessionStorageService;
        private const string JWT_Key = nameof(JWT_Key);

        private string? _jwtCache;

        public event Action<string?>? LoginChange;

        public AuthenticationService(IHttpClientFactory factory, ISessionStorageService sessionStorageService)
        {
            _factory = factory;
            _sessionStorageService = sessionStorageService;
        }


        public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(JWT_Key);
            return _jwtCache;
        }

        public async Task LogoutAsync()
        {
            await _sessionStorageService.RemoveItemAsync(JWT_Key);

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
            var response = await _factory.CreateClient("ServerApi").PostAsync("api/authentication/login", JsonContent.Create(model));


            if (!response.IsSuccessStatusCode)
                throw new UnauthorizedAccessException("Đăng nhập thất bại!");

            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (content == null) throw new InvalidDataException();

            await _sessionStorageService.SetItemAsync(JWT_Key, content.JwtToken);

            LoginChange?.Invoke(GetUsername(content.JwtToken));

            return content.Expiration;
        }
    }
}
