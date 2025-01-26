namespace Trendyum.Common.Models.Auth;

public class UserRegisterRequest
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}
