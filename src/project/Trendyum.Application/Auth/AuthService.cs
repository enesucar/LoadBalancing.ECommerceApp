using Microsoft.AspNetCore.Identity;
using Shared.Guids;
using Shared.Security.Tokens;
using Trendyum.Application.Interfaces.Auth;
using Trendyum.Application.Roles;
using Trendyum.Common.Models.Auth;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IGuidGenerator _guidGenerator;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenService tokenService,
        IGuidGenerator guidGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _guidGenerator = guidGenerator;
    }

    public async Task<UserLoginResponse> LoginAsync(UserLoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new UserLoginResponse()
            {
                Result = "Failed"
            };
        }
        
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (!signInResult.Succeeded)
        {
            return new UserLoginResponse()
            {
                Result = signInResult.ToString()
            };
        }

        var token = _tokenService.CreateAccessToken(new AccessTokenClaims()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Roles = (await _userManager.GetRolesAsync(user)).ToArray()
        });

        return new UserLoginResponse()
        {
            Result = signInResult.ToString(),
            AccessToken = new UserLoginAccessTokenResponse()
            {
                AccessToken = token.Token,
                Expiration = token.Expiration
            }
        };
    }

    public async Task<UserRegisterResponse> RegisterAsync(UserRegisterRequest request)
    {
        var user = new User()
        {
            Id = _guidGenerator.Generate(),
            Email = request.Email,
            CreationDate = DateTime.Now
        };

        var createUserResult = await _userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
        {
            return new UserRegisterResponse()
            {
                Errors = Convert(createUserResult.Errors)
            };
        }

        var addToRoleResult = await _userManager.AddToRolesAsync(user, [RoleConstants.User]);
        if (!addToRoleResult.Succeeded)
        {
            return new UserRegisterResponse()
            {
                Errors = Convert(addToRoleResult.Errors)
            };
        }

        return new UserRegisterResponse()
        {
            Succeeded = true,
            Errors = null
        };
    }

    private List<UserRegisterErrorsResponse> Convert(IEnumerable<IdentityError> identityErrors)
    {
        return identityErrors.Select(error => new UserRegisterErrorsResponse()
        {
            Code = error.Code,
            Description = error.Description
        }).ToList();
    }
}
