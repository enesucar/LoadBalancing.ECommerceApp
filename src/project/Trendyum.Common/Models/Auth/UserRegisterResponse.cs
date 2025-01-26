namespace Trendyum.Common.Models.Auth;

public class UserRegisterResponse
{
    public bool Succeeded { get; set; }

    public List<UserRegisterErrorsResponse>? Errors { get; set; } = null;
}

public class UserRegisterErrorsResponse
{
    public string Code { get; set; } = default!;

    public string Description { get; set; } = default!;
}
