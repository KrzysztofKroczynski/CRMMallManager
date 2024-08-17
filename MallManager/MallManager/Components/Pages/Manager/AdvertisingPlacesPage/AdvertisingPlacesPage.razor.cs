using Microsoft.AspNetCore.Components;

namespace MallManager.Components.Pages.Manager.AdvertisingPlacesPage;

public partial class AdvertisingPlacesPage : ComponentBase
{
    private readonly List<AdvertisingRequest> exampleList = new()
    {
        new AdvertisingRequest
        {
            TenantNumber = "1001",
            SelectedMonth = "Styczeń",
            IsInTheCenter = true,
            IsDuringPeakHours = true,
            Beneficiary = "Firma XYZ",
            Description = "Reklama nowego produktu",
            IsAnimated = true,
            ReplayOfThePreviousCampaign = false,
            DurningWorkingDays = true,
            DurningWeekend = false,
            Status = "Aktywny"
        },
        new AdvertisingRequest
        {
            TenantNumber = "1002",
            SelectedMonth = "Luty",
            IsInTheCenter = false,
            IsDuringPeakHours = false,
            Beneficiary = "Sklep ABC",
            Description = "Promocja weekendowa",
            IsAnimated = false,
            ReplayOfThePreviousCampaign = true,
            DurningWorkingDays = false,
            DurningWeekend = true,
            Status = "Oczekujący"
        }
    };

    private bool ShowCompleted { get; set; }

    private IEnumerable<AdvertisingRequest> FilteredExampleList =>
        ShowCompleted ? exampleList : exampleList.Where(x => x.Status != "Zakończony");

    private void AcceptAdvertisingRequest(AdvertisingRequest request)
    {
        // TODO: Business logic
    }

    private void RejectAdvertisingRequest(AdvertisingRequest request)
    {
        // TODO: Business logic
    }
}

public class AdvertisingRequest
{
    public string TenantNumber { get; set; }
    public string SelectedMonth { get; set; }
    public bool IsInTheCenter { get; set; }
    public bool IsDuringPeakHours { get; set; }
    public string Beneficiary { get; set; }
    public string Description { get; set; }
    public bool IsAnimated { get; set; }
    public bool ReplayOfThePreviousCampaign { get; set; }
    public bool DurningWorkingDays { get; set; }
    public bool DurningWeekend { get; set; }
    public string Status { get; set; }
}