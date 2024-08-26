using Microsoft.AspNetCore.Components;
using Shared.Core.Entities;

namespace MallManager.Components.Pages.Manager.RentalsOfTenantsPage;

public partial class RentalsOfTenantsPage : ComponentBase
{
    public bool ShowLeaseApplications { get; set; } = true;
    public bool ShowMassEvents { get; set; }
    public bool ShowMarketingCampaign { get; set; }
    
    private IEnumerable<LeaseApplication> _leaseApplications = Enumerable.Empty<LeaseApplication>();
    private IEnumerable<MassEvent> _massEvents = Enumerable.Empty<MassEvent>();
    private IEnumerable<MarketingCampaign> _marketingCampaigns = Enumerable.Empty<MarketingCampaign>();
    
    
    protected override async Task OnInitializedAsync()
    {
        // PLACEHOLDER
        await AnswerRequestsService.LoadRequestsAsync();

        _leaseApplications = AnswerRequestsService.LeaseApplications;
        _massEvents = AnswerRequestsService.MassEvents;
        _marketingCampaigns = AnswerRequestsService.MarketingCampaigns;
    }

    public void ShowSpecificTypeOfRequests(bool showLeaseApplications, bool showMarketingCampaigns, bool showMassEvents)
    {
        if ((showLeaseApplications && showMarketingCampaigns) || (showLeaseApplications && showMassEvents) ||
            (showMarketingCampaigns && showMassEvents))
        {
            throw new ArgumentException("Two or all three values are true. Only one value should be true");
        } 
        
        if (!showLeaseApplications && !showMarketingCampaigns && !showMassEvents)
        {
            ShowLeaseApplications = true;
            ShowMassEvents = false;
            ShowMarketingCampaign = false;
        }
        else
        {
            ShowLeaseApplications = showLeaseApplications;
            ShowMassEvents = showMarketingCampaigns;
            ShowMarketingCampaign = showMassEvents;
        }
        
        StateHasChanged();
    }
    
    private void EditRequest()
    {
        // Business logic to edit request
    }

    public async Task AcceptLeaseApplication(LeaseApplication leaseApplication)
    {
        await AnswerRequestsService.AcceptLeaseApplication(leaseApplication);
    }
    public async Task DenyLeaseApplication(LeaseApplication leaseApplication)
    {
        await AnswerRequestsService.DenyLeaseApplication(leaseApplication);
    }
    
    private async Task AcceptMarketingCampaign(MarketingCampaign marketingCampaign)
    {
        await AnswerRequestsService.AcceptMarketingCampaign(marketingCampaign);
    }
    private async Task DenyMarketingCampaign(MarketingCampaign marketingCampaign)
    {
        await AnswerRequestsService.DenyMarketingCampaign(marketingCampaign);
    }
    
    private async Task AcceptMassEvent(MassEvent massEvent)
    {
        await AnswerRequestsService.AcceptMassEvent(massEvent);
    }
    private async Task DenyMassEvent(MassEvent massEvent)
    {
        await AnswerRequestsService.DenyMassEvent(massEvent);
    }
}

public class ExampleRentalModel
{
    public string RentalNumber { get; set; }
    public string ApartmentNumber { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime DateOfNextRentPayment { get; set; }
    public decimal RentAmount { get; set; }
    public decimal LastRevenueAmount { get; set; }
    public DateTime NextRevenueSubmissionDate { get; set; }
}