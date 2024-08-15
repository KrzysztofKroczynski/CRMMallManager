using System.Reflection;
using Ardalis.SharedKernel;
using MallManager.Infrastructure.Persistence;
using MallManager.Infrastructure.UserState;
using MallManager.Infrastructure.UserState.PersonalInfoStateService;
using Shared.Core.Entities;
using Shared.Web;

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
}