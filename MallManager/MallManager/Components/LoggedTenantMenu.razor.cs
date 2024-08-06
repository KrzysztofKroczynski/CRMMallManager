using Microsoft.AspNetCore.Components;

namespace MallManager.Components;

public partial class LoggedTenantMenu : LayoutComponentBase
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }
    
    bool _drawerOpen = false;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }
}