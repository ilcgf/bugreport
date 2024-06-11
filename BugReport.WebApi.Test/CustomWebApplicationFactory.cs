using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Data.Common;

namespace BugReport.WebApi.Test;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private DbConnection _connection = null!;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);


        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
            services.RemoveAll(typeof(DbConnection));

            services.AddSingleton<DbConnection>(provider =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                return connection;
            });

            

            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                //options.UseSqlite(provider.GetRequiredService<DbConnection>());
                options.UseSqlite(_connection);
            });


            var provider = services.BuildServiceProvider();

            _connection = provider.GetRequiredService<DbConnection>();
            PopulateDatabase(provider);
        });
    }

    private void PopulateDatabase(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();

        User[] users =
        [
            new(){Id=1,Name="Anton"},
            new(){Id=2,Name="Bob"},
            new(){Id=3,Name="Charlie"},
            new(){Id=4,Name="David"},
        ];
        context.Users.AddRange(users);
        context.SaveChanges();
    }
}