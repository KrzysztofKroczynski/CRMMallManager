namespace MallManager.Infrastructure.Configuration;

public sealed class SmtpConfiguration
{
    public const string SectionName = "SmtpConfiguration";

    public string Url { get; set; } = string.Empty;

    public int Port { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}