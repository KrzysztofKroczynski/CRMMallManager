using System.Reflection;
using Shared.Core.Entities;

namespace MallManager.Client;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(UserInfo)), // MallManager.Client
            Assembly.GetAssembly(typeof(Person)) // Shared
        };
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatRAssemblies!));
        
        
        return services;
    }
}