using Trendyum.Common.Models.Auth;

namespace Trendyum.Blazor.Interfaces;

public interface IAuthService
{
    Task<UserLoginResponse> LoginAsync(UserLoginRequest request);

    Task<UserRegisterResponse> RegisterAsync(UserRegisterRequest request);
}
