namespace MallManager.Infrastructure.Configuration.Options;

public class DbConnectionString
{
    public const string SectionName = "ConnectionStrings";

    public string DefaultConnection { get; set; } = string.Empty;
}