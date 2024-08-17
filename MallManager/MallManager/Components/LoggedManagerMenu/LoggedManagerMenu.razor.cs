using Microsoft.AspNetCore.Components;

namespace MallManager.Components.LoggedManagerMenu;

public partial class LoggedManagerMenu : LayoutComponentBase
{
    private bool _drawerOpen = false;

    [Parameter] public RenderFragment ChildContent { get; set; }

    private void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}