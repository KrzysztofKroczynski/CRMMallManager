using MudBlazor;
using MudBlazor.Utilities;


namespace MallManager.Components.Layout;

public class MM_Palette : Palette
{
    public MudColor MM_NavyBlue { get; set; }
    public MudColor MM_LightBgColor { get; set; }

}

public class MM_Theme : MudTheme
{
    public MM_Theme()
    {
        var Palette = new MM_Palette
        {
            MM_NavyBlue = MM_Colors.MM_NavyBlue,
            MM_LightBgColor = MM_Colors.MM_LightBgColor
            
        };
    }
}
