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
    private SignInResult? _result;
    private Severity _severity;
    private bool _submitted;

    [Inject] private LoginHandler LoginHandler { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
    [SupplyParameterFromQuery] private string? ReturnUrl { get; set; }


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

    private async Task OnValidSubmit()
    {
        try
        {
            _result = await LoginHandler.LoginUser(_model);
            _message = string.Empty;
            _submitted = true;

            if (_result.Succeeded)
            {
                NavigationManager.NavigateTo(ReturnUrl ?? "/", true);
                return;
            }

            if (_result.IsLockedOut)
            {
                _severity = Severity.Warning;
                NavigationManager.NavigateTo("Account/Lockout", true);
                return;
            }

            _severity = Severity.Warning;
            _message = "Invalid login attempt.";
        }
        catch (Exception ex)
        {
            _submitted = true;
            _severity = Severity.Error;
            _message = "An error occurred during login: " + ex;
        }
    }
}