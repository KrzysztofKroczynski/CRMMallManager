using MallManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Shared.Web;

namespace MallManager.UseCases.UserRegistration;

public sealed class RegistrationHandler
{
    private readonly IEmailSender<ApplicationUser> _emailSender;
    private readonly ILogger<RegistrationHandler> _logger;
    private readonly NavigationManager _navigationManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;

    private IEnumerable<IdentityError>? identityErrors;


    public RegistrationHandler(
        UserManager<ApplicationUser> userManager,
        IUserStore<ApplicationUser> userStore,
        IEmailSender<ApplicationUser> emailSender,
        ILogger<RegistrationHandler> logger,
        NavigationManager navigationManager
    )
    {
        _userManager = userManager;
        _userStore = userStore;
        _emailSender = emailSender;
        _logger = logger;
        _navigationManager = navigationManager;
    }


    public async Task RegisterUserAsync(RegistrationForm form)
    {
        var user = CreateUser();

        await _userStore.SetUserNameAsync(user, form.Email, CancellationToken.None);
        var emailStore = GetEmailStore();
        await emailStore.SetEmailAsync(user, form.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, form.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(
                $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        _logger.LogInformation("User created a new account with password.");
        // TO DO WYSYŁANIE MAILA

        // String userId = await _userManager.GetUserIdAsync(user);
        // String code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        // String callbackUrl = _navigationManager.GetUriWithQueryParameters(
        //     _navigationManager.ToAbsoluteUri("/Login").AbsoluteUri,
        //     new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code });
        //
        // await _emailSender.SendConfirmationLinkAsync(user, form.Email, HtmlEncoder.Default.Encode(callbackUrl)); Object reference not set to an instance of an object.

        if (_userManager.Options.SignIn.RequireConfirmedAccount)
        {
            // _redirectManager.RedirectTo(
            //     "/EmailSent",
            //     new Dictionary<string, object?> { ["email"] = form.Email });
        }
    }

    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                                                $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
        }
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email support.");

        return (IUserEmailStore<ApplicationUser>)_userStore;
    }
}