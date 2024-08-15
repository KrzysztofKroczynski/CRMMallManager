using MudBlazor;
using MudBlazor.Utilities;

namespace MallManager.Components.Layout;

public class MmPalette : Palette
{
    public required MudColor MmNavyBlue { get; set; }
    public required MudColor MmLightBgColor { get; set; }
}

public class MmTheme : MudTheme
{
    public MmTheme()
    {
        var palette = new MmPalette
        {
            MmNavyBlue = MM_Colors.MM_NavyBlue,
            MmLightBgColor = MM_Colors.MM_LightBgColor
        };
    }
}