﻿using System.Reflection;
using Ardalis.SharedKernel;
using MallManager.Infrastructure.Configuration.HttpClients;
using Ardalis.Specification;
using MallManager.Infrastructure.Configuration.HttpClients;
using MallManager.Infrastructure.Persistence;
using MallManager.Infrastructure.UserState;
using MallManager.Infrastructure.UserState.ClaimsTransformation;
using MallManager.Infrastructure.UserState.PersonalInfoStateService;
using Serilog;
using Microsoft.AspNetCore.Authentication;
using Serilog;
using Shared.Core.Entities;
using Shared.Web.FormModels;

namespace MallManager.Infrastructure;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        ConfigurationManager config)
    {
        services.AddScoped(typeof(IRepositoryBase<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepositoryBase<>), typeof(Repository<>));

        var mediatRAssemblies = new[]
        {
            Assembly.GetAssembly(typeof(Person)), // Shared
            Assembly.GetAssembly(typeof(ApplicationUser)) // MallManager
        };

        services.AddScoped<IClaimsTransformation, UserRoleAdder>();

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