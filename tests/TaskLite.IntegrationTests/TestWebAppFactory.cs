using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskLite.Infrastructure.Persistence;

namespace TaskLite.IntegrationTests;

public class TestWebAppFactory
{
    //protected override void ConfigureWebHost(IWebHostBuilder builder)
    //{
    //    builder.ConfigureAppConfiguration((context, config) =>
    //    {
    //        config.AddJsonFile("appsettings.json", optional: true)
    //              .AddInMemoryCollection(new Dictionary<string, string?>
    //              {
    //                  ["Jwt:Key"] = "SuperStrongJwtSecretKey_1234567890!@#$",
    //                  ["Jwt:Issuer"] = "TaskLiteApi",
    //                  ["Jwt:Audience"] = "TaskLiteClient"
    //              });
    //    });

    //    builder.ConfigureServices(services =>
    //    {
    //        services.AddDbContext<AppDbContext>(options =>
    //            options.UseInMemoryDatabase("TaskLiteTestDb"));
    //    });
    //}
}