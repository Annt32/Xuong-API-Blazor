using AppData.Authentication;

namespace BlazorServer.IServices
{
    public interface IAuthenticationService
    {
        event Action<string?>? LoginChange;

        ValueTask<string> GetJwtAsync();
        Task<DateTime> LoginAsync(LoginModel model);
        Task LogoutAsync();
    }
}