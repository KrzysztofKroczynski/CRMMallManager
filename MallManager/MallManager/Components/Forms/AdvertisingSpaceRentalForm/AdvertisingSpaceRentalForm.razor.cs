using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Forms.AdvertisingSpaceRentalForm;

public partial class AdvertisingSpaceRentalForm : ComponentBase
{
    private string? _beneficiary;
    private string? _description;
    private bool? _durningWeekend = true;
    private bool? _durningWorkingDays = true;
    private bool? _isAnimated = false;
    private bool? _isDuringPeakHours = true;
    private bool? _isInTheCenter = false;
    private bool? _replayOfThePreviousCampaign = false;

    private string? _selectedMonth;

    private List<string> months = new()
    {
        "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec",
        "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
    };
}