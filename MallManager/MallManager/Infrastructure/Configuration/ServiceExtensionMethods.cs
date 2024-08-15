namespace MallManager.Infrastructure.Configuration;

public static class ServiceExtensionMethods
{
    public static IServiceCollection RegisterConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SmtpConfiguration>(configuration.GetSection(SmtpConfiguration.SectionName));

        return services;
    }
}