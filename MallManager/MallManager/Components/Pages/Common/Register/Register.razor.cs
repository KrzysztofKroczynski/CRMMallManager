using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Web;

namespace MallManager.Components.Pages.Common.Register;

public partial class Register : ComponentBase
{
    private RegistrationForm model = new();
    bool success;

    private void OnValidSubmit(EditContext context)
    {
        success = true;
        StateHasChanged();
    }
}