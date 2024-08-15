using System.Net;
using System.Net.Mail;
using MallManager.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Shared.Core.Interfaces;

namespace MallManager.Infrastructure.Email;

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly ILogger<SmtpEmailSender> _logger;
    private readonly NetworkCredential _networkCredential;
    private readonly SmtpClient _smtpClient;

    public SmtpEmailSender(IOptions<SmtpConfiguration> options, ILogger<SmtpEmailSender> logger)
    {
        _logger = logger;
        var smtpConfiguration = options.Value;
        _smtpClient = new SmtpClient(smtpConfiguration.Url, smtpConfiguration.Port);
        _networkCredential = new NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password);
    }

    public async Task SendEmailAsync(string to, string from, string subject, string body)
    {
        var message = new MailMessage
        {
            From = new MailAddress(from),
            Subject = subject,
            Body = body
        };
        message.To.Add(new MailAddress(to));
        await _smtpClient.SendMailAsync(message);

        _logger.LogInformation("Sent email {to} from {from} with subject {subject}.", to, from, subject);
    }
}