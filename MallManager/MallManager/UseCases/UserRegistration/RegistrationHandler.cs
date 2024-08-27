using MallManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Shared.Web.FormModels;

namespace MallManager.UseCases.UserRegistration;

public sealed class RegistrationHandler(
    UserManager<ApplicationUser> userManager,
    IUserStore<ApplicationUser> userStore,
    IEmailSender<ApplicationUser> emailSender,
    ILogger<RegistrationHandler> logger
)
{
    private readonly IEmailSender<ApplicationUser> _emailSender = emailSender;

    public async Task RegisterUserAsync(RegistrationForm form)
    {
        var user = CreateUser();

        await userStore.SetUserNameAsync(user, form.Email, CancellationToken.None);
        IUserEmailStore<ApplicationUser> emailStore = GetEmailStore();
        await emailStore.SetEmailAsync(user, form.Email, CancellationToken.None);
        IdentityResult result = await userManager.CreateAsync(user, form.Password);
        if (!result.Succeeded)
            throw new InvalidOperationException(
                $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");

        logger.LogInformation("User created a new account with password.");
        await AddRoleToUser(user, "Client");
        // TO DO WYSYŁANIE MAILA

        // String userId = await _userManager.GetUserIdAsync(user);
        // String code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        // String callbackUrl = _navigationManager.GetUriWithQueryParameters(
        //     _navigationManager.ToAbsoluteUri("/Login").AbsoluteUri,
        //     new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code });
        //
        // await _emailSender.SendConfirmationLinkAsync(user, form.Email, HtmlEncoder.Default.Encode(callbackUrl)); Object reference not set to an instance of an object.

        if (userManager.Options.SignIn.RequireConfirmedAccount)
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
        if (!userManager.SupportsUserEmail)
            throw new NotSupportedException("The default UI requires a user store with email support.");

        return (IUserEmailStore<ApplicationUser>)userStore;
    }

    public async Task AddRoleToUser(ApplicationUser user, string roleName)
    {
        var result = await userManager.AddToRoleAsync(user, roleName);

        if (result.Succeeded)
        {
            logger.LogInformation("Added role to user");
            Console.Beep();
            return;
        }

        logger.LogInformation("Could not add role to user: " + result);
    }
}