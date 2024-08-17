using Microsoft.AspNetCore.Components;

namespace MallManager.Components.LoggedTenantMenu;

public partial class LoggedTenantMenu : LayoutComponentBase
{
    private bool _drawerOpen = false;

    [Parameter] public RenderFragment ChildContent { get; set; }

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}