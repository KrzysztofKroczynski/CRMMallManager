using System.Collections.Concurrent;
using MallManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace MallManager.UseCases.Login;

public class LoginInfo
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public class LoginMiddleware(RequestDelegate next)
{
    public static IDictionary<Guid, LoginInfo> Logins { get; }
        = new ConcurrentDictionary<Guid, LoginInfo>();

    public async Task Invoke(HttpContext context, SignInManager<ApplicationUser> signInManager)
    {
        if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
        {
            var key = Guid.Parse(context.Request.Query["key"]!);
            var info = Logins[key];

            var result = await signInManager.PasswordSignInAsync(info.Email, info.Password, false, true);
            info.Password = string.Empty;
            if (result.Succeeded)
            {
                Logins.Remove(key);
                context.Response.Redirect("/");
                return;
            }

            if (result.RequiresTwoFactor)
            {
                context.Response.Redirect("/LoginWith2fa" + key);
                return;
            }

            context.Response.Redirect("/login");
            return;
        }

        await next.Invoke(context);
    }
}