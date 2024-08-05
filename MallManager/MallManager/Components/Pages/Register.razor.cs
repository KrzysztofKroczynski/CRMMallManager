
using Microsoft.AspNetCore.Components;

using MallManager.Components.Forms;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace MallManager.Components.Pages;

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