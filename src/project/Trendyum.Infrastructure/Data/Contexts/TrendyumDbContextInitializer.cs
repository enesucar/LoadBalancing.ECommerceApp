using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Guids;
using Trendyum.Application.Roles;
using Trendyum.Domain.Entities;
using Trendyum.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.AspNetCore.Builder;

public static class WebApplicationExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<TrendyumDbContextInitializer>();
        await initializer.InitializeAsync();
        await initializer.SeedAsync();
    }
}

public class TrendyumDbContextInitializer
{
    private readonly ILogger<TrendyumDbContextInitializer> _logger;
    private readonly TrendyumDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IGuidGenerator _guidGenerator;

    public TrendyumDbContextInitializer(
        ILogger<TrendyumDbContextInitializer> logger,
        TrendyumDbContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IGuidGenerator guidGenerator)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _guidGenerator = guidGenerator;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        await CreateRoleAsync(new Role(RoleConstants.Administrator));
        await CreateRoleAsync(new Role(RoleConstants.User));

        var administrator = new User
        {
            Id = _guidGenerator.Generate(),
            Email = "admin@trendyum.com",
            EmailConfirmed = true,
            SecurityStamp = _guidGenerator.Generate().ToString(),
            CreationDate = DateTime.Now
        };

        var user = new User
        {
            Id = _guidGenerator.Generate(),
            Email = "enes@trendyum.com",
            EmailConfirmed = true,
            SecurityStamp = _guidGenerator.Generate().ToString(),
            CreationDate = DateTime.Now
        };

        await CreateUserAsync(administrator, "enes1234", [RoleConstants.Administrator]);
        await CreateUserAsync(user, "enes1234", [RoleConstants.User]);

        Category category1 = new Category()
        {
            Id = _guidGenerator.Generate(),
            Name = "Electronics"
        };

        Category category2 = new Category()
        {
            Id = _guidGenerator.Generate(),
            Name = "Home & Kitchen"
        };

        Category category3 = new Category()
        {
            Id = _guidGenerator.Generate(),
            Name = "Automotive"
        };

        Category category4 = new Category()
        {
            Id = _guidGenerator.Generate(),
            Name = "Arts & Crafts"
        };
        Category category5 = new Category()
        {
            Id = _guidGenerator.Generate(),
            Name = "Furniture"
        };

        await CreateCategory(category1);
        await CreateCategory(category2);
        await CreateCategory(category3);
        await CreateCategory(category4);
        await CreateCategory(category5);
    }

    private async Task CreateRoleAsync(Role role)
    {
        if (_roleManager.Roles.All(r => r.Name != role.Name))
        {
            await _roleManager.CreateAsync(role);
        }
    }

    private async Task CreateUserAsync(User user, string password, string[] roles)
    {
        if (_userManager.Users.All(u => u.Email != user.Email))
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRolesAsync(user, roles);
        }
    }

    private async Task CreateCategory(Category category)
    {
        if (!_context.Categories.Any(o => o.Name == category.Name))
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
    }
}
