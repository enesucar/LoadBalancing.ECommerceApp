namespace Trendyum.Common.Models.Auth;

public class UserLoginRequest
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
