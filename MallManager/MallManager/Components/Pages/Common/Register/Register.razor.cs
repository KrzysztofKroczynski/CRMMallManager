using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Web.FormModels;

namespace MallManager.Components.Pages.Common.Register;

public partial class Register : ComponentBase
{
    private readonly RegistrationForm model = new();
    private bool success;

    private void OnValidSubmit(EditContext context)
    {
        success = true;
        StateHasChanged();
    }
}