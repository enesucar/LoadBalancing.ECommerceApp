﻿using Microsoft.AspNetCore.Identity;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Auth;

public class CustomUserValidator : UserValidator<User>
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var result = await base.ValidateAsync(manager, user);
        if (result.Succeeded)
        {
            return result;
        }

        var errors = result.Errors.ToList();
        if (user.UserName.IsNullOrWhiteSpace())
        {
            var expectedError = Describer.InvalidUserName(user.UserName);
            errors.RemoveAll(x => x.Code == expectedError.Code);
        }

        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }
}
