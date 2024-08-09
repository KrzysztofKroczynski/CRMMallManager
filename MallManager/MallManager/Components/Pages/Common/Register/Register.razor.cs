using MallManager.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MallManager.Components.Pages.Common.Register;

public partial class Register : ComponentBase
{
    bool success;
    private RegistrationForm model = new();
    private void OnValidSubmit(EditContext context)
    {
        success = true;
        StateHasChanged();
    }
}