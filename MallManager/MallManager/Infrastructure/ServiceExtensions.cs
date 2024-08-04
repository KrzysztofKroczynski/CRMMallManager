using System.Reflection;
using Ardalis.SharedKernel;
using MallManager.Data;
using MallManager.Data.Entities;
using MallManager.Infrastructure.Persistence;
using Shared.Core.Entities;

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


        return services;
    }
}