using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace MallManager.Components.Pages;

public partial class NewRevenueReportPage : ComponentBase
{
    private MudForm? _form = new();
    public string? Comment { get; set; }
    public double? Income { get; set; }

    private void SubmitRevenueReport()
    {
        // TODO: Business logic
    }
}