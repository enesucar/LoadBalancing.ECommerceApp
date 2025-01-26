namespace Trendyum.Common.Models.Auth;

public class UserLoginResponse
{
    public string Result { get; set; } = default!;

    public UserLoginAccessTokenResponse? AccessToken { get; set; }
}

public class UserLoginAccessTokenResponse
{
    public string AccessToken { get; set; } = default!;

    public DateTime Expiration { get; set; } = default!;
}