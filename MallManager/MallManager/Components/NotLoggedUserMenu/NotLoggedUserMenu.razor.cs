using Microsoft.AspNetCore.Components;

namespace MallManager.Components.NotLoggedUserMenu;

public partial class NotLoggedUserMenu : LayoutComponentBase
{
    private bool _drawerOpen = false;

    [Parameter] public RenderFragment ChildContent { get; set; }

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}