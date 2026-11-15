using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Helpdesk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HelpDeskConnection") 
            ?? throw new InvalidOperationException("Empty connection string");

        services.AddDbContext<ApplicationDbContext>(options => ConfigureDatabase(options, connectionString));
        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>(tags: ["PostgreSQL", "HelpDeskDbContext"]);

        return services;
    }

    public static void ConfigureDatabase(DbContextOptionsBuilder options, string connectionString)
    {
        options.UseNpgsql(connectionString, npgsql => 
        {
            npgsql.MigrationsHistoryTable("__EFMigrationsHistory", "helpdesk");
        });
    }
}
