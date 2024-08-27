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
    private SignInResult _result;
    private Severity _severity;
    private bool _submitted;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
    [Inject] private UserManager<ApplicationUser> UserManager { get; set; } = null!;
    [Inject] private SignInManager<ApplicationUser> SignInManager { get; set; } = null!;


    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            // Przekierowanie, jeśli użytkownik jest już zalogowany
            NavigationManager.NavigateTo("/");
        }
    }

    // TODO: rozdzielić to na serwis i nazwać middleware (LoginHandler) jakoś sensowniej.
    private async Task OnValidSubmit()
    {
        ApplicationUser? user = await UserManager.FindByEmailAsync(_model.Email);
        if (user == null)
        {
            _message = "Email nie poprawny";
            _severity = Severity.Warning;
            _submitted = true;
            return;
        }

        if (await SignInManager.CanSignInAsync(user))
        {
            SignInResult result = await SignInManager.CheckPasswordSignInAsync(user, _model.Password, false);
            if (result == SignInResult.Success)
            {
                Console.WriteLine("User: " + user.Email + " successful login.");
                Guid key = Guid.NewGuid();
                LoginHandler.Logins[key] = new LoginInfo { Email = _model.Password, Password = _model.Password };
                NavigationManager.NavigateTo($"/login?key={key}", true);
            }
            else
            {
                Console.WriteLine("User: " + user.Email + " failed login.");
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