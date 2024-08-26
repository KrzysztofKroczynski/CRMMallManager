using MallManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Shared.Web.FormModels;

namespace MallManager.UseCases.Login;

public sealed class LoginHandler(
    SignInManager<ApplicationUser> signInManager,
    ILogger<LoginHandler> logger)
{
    public async Task<SignInResult> LoginUser(LoginForm form)
    {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true

        SignInResult result =
            await signInManager.PasswordSignInAsync(form.Email, form.Password, form.RememberMe,
                lockoutOnFailure: false);
        Console.Beep();
        if (result.Succeeded)
        {
            logger.LogInformation("User logged in.");
        }
        else if (result.IsLockedOut)
        {
            logger.LogWarning("User account locked out.");
        }
        else
        {
            logger.LogWarning("Error: Invalid login attempt.");
        }

        return result;
    }
}