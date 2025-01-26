namespace Shared.Security.Tokens;

public class AccessTokenOptions
{
    public string Audience { get; set; }

    public string Issuer { get; set; }

    public int Expiration { get; set; }

    public string SecurityKey { get; set; }

    public AccessTokenOptions()
    {
        Audience = default!;
        Issuer = default!;
        SecurityKey = default!;
    }
}
