using Dummy.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Dummy.Integration.Tests
{

    public class CustomWebApplicationFactory<TStartup, TDbContext>
        : WebApplicationFactory<TStartup> 
            where TStartup : class
            where TDbContext : DbContext
    {        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {            
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TDbContext>));

                services.Remove(descriptor);

                services.AddDbContext<TDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<TDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup, TDbContext>>>();

                    db.Database.EnsureCreated();

                }
            });
        }
    }
}
