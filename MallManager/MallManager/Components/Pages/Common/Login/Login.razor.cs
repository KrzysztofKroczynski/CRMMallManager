using MallManager.Infrastructure.Persistence;
using MallManager.UseCases.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MudBlazor;
using Shared.Web.FormModels;

namespace MallManager.Components.Pages.Common.Login;

public partial class Login : ComponentBase
{
    private readonly LoginForm _model = new();
    private string _message = string.Empty;
    private Severity _severity;
    private bool _submitted;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
    [Inject] private UserManager<ApplicationUser> UserManager { get; set; } = null!;
    [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; } = null!;

    private async Task OnValidSubmit()
    {
        var usr = await UserManager.FindByEmailAsync(_model.Email);
        if (usr == null)
        {
            _message = "User not found";
            _severity = Severity.Warning;
            _submitted = true;
            return;
        }


        if (await SignInManager.CanSignInAsync(usr))
        {
            var result = await SignInManager.CheckPasswordSignInAsync(usr, _model.Password, true);
            if (result == SignInResult.Success)
            {
                var key = Guid.NewGuid();
                LoginMiddleware.Logins[key] = new LoginInfo { Email = _model.Email, Password = _model.Password };
                NavigationManager.NavigateTo($"/login?key={key}", true);
            }
            else
            {
                _message = "Login failed. Check your password.";
                _severity = Severity.Warning;
                _submitted = true;
            }
        }
        else
        {
            _message = "Your account is blocked";
            _severity = Severity.Warning;
            _submitted = true;
        }
    }
}