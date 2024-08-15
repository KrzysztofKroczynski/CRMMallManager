using System.Reflection;
using Ardalis.SharedKernel;
using MallManager.Infrastructure.Configuration.HttpClients;
using MallManager.Infrastructure.Persistence;
using MallManager.Infrastructure.UserState;
using MallManager.Infrastructure.UserState.PersonalInfoStateService;
using Serilog;
using Shared.Core.Entities;
using Shared.Web.FormModels;

namespace MallManager.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        ConfigurationManager config)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Person)), // Shared
            Assembly.GetAssembly(typeof(ApplicationUser)) // MallManager
        };

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
        services.AddSingleton<BaseStateService<PersonalForm>, PersonalInfoStateService>();
        return services;
    }

    public static IServiceCollection AddNamedHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(HttpClientsConfig.SectionName);
        var httpClientsConfig = new HttpClientsConfig();
        section.Bind(httpClientsConfig);

        //AddHttpClients here

        return services;
    }

    public static void AddLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();
    }
}