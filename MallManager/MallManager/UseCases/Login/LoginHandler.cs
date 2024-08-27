using System.Collections.Concurrent;
using MallManager.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace MallManager.UseCases.Login;

public class LoginInfo
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginHandler
{
    private readonly RequestDelegate _next;

    public LoginHandler(RequestDelegate next)
    {
        _next = next;
    }

    public static IDictionary<Guid, LoginInfo> Logins { get; } = new ConcurrentDictionary<Guid, LoginInfo>();

    public async Task InvokeAsync(HttpContext context, SignInManager<ApplicationUser> signInMgr)
    {
        if (context.Request.Path == "/login" && context.Request.Query.ContainsKey("key"))
        {
            var key = Guid.Parse(context.Request.Query["key"]);
            if (Logins.TryGetValue(key, out var info))
            {
                var result = await signInMgr.PasswordSignInAsync(info.Email, info.Password, false, true);
                info.Password = null;

                if (result.Succeeded)
                {
                    Logins.Remove(key);
                    context.Response.Redirect("/");
                    return;
                }

                if (result.RequiresTwoFactor)
                {
                    context.Response.Redirect($"/loginwith2fa/{key}");
                    return;
                }

                // Redirect to a custom error page
                return;
            }
        }

        await _next(context);
    }
}