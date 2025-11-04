using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskLite.Domain.Entities;
using TaskLite.Infrastructure.Persistence;

namespace TaskLite.UnitTests.Identity;

public class IdentityTestFixture
{
    public AppDbContext DbContext { get; }
    public UserManager<User> UserManager { get; }
    public SignInManager<User> SignInManager { get; }

    public IdentityTestFixture()
    {
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("TaskLiteTestDb"));

        services.AddLogging();
        services.AddHttpContextAccessor();

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        var provider = services.BuildServiceProvider();
        DbContext = provider.GetRequiredService<AppDbContext>();
        UserManager = provider.GetRequiredService<UserManager<User>>();
        SignInManager = provider.GetRequiredService<SignInManager<User>>();
    }
}
