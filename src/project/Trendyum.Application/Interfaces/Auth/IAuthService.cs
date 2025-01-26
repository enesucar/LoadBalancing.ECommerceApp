using Trendyum.Common.Models.Auth;

namespace Trendyum.Application.Interfaces.Auth;

public interface IAuthService
{
    Task<UserLoginResponse> LoginAsync(UserLoginRequest request);

    Task<UserRegisterResponse> RegisterAsync(UserRegisterRequest request);
}
