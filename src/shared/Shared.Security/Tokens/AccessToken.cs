namespace Shared.Security.Tokens;

public class AccessToken
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }

    public AccessToken()
    {
        Token = default!;
    }
}
