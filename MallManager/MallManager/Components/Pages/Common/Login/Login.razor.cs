using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Shared.Web.FormModels;

namespace MallManager.Components.Pages.Common.Login;

public partial class Login : ComponentBase
{
    private readonly LoginForm _model = new();
    private string _message = string.Empty;
    private Severity _severity;
    private bool _success = true;


    private async Task OnValidSubmit(EditContext context)
    {
        try
        {
            _success = true;
            _message = string.Empty;
        }
        catch (Exception ex)
        {
            _success = false;
            _severity = Severity.Error;
            _message = ex.Message;
        }
    }
}