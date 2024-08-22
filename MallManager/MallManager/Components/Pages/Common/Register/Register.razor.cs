using MallManager.Components.Account;
using MallManager.UseCases.UserRegistration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using RegistrationForm = Shared.Web.RegistrationForm;

namespace MallManager.Components.Pages.Common.Register;

public partial class Register : ComponentBase
{
    private readonly RegistrationForm _model = new();
    private string _errorMessage = string.Empty;
    private bool _success;

    [Inject] private RegistrationHandler _registerUserHandler { get; set; } = null!;

    [Inject] private IdentityRedirectManager _redirectManager { get; set; } = null!;

    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            await _registerUserHandler.RegisterUserAsync(_model);
            _success = true;
            _errorMessage = string.Empty;
        }
        catch (Exception ex)
        {
            _success = false;
            _errorMessage = ex.Message;
        }

        StateHasChanged();
        if (_success) _redirectManager.RedirectTo("/EmailSent");
    }
}