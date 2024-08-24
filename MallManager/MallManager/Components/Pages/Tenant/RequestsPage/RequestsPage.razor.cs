using Microsoft.AspNetCore.Components;
using Shared.Core.Entities;

namespace MallManager.Components.Pages.Tenant.RequestsPage;

public partial class RequestsPage : ComponentBase
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
        var userId = 2;
        await ManageRequestsService.LoadAspNetUserRequestsAsync(userId);

        _leaseApplications = ManageRequestsService.LeaseApplications;
        _massEvents = ManageRequestsService.MassEvents;
        _marketingCampaigns = ManageRequestsService.MarketingCampaigns;
    }

    public void ShowLeaseApplicationsOnScreen()
    {
        ShowLeaseApplications = true;
        ShowMassEvents = false;
        ShowMarketingCampaign = false;
        
        StateHasChanged();
    }
    
    public void ShowMarketingCampaignsOnScreen()
    {
        ShowLeaseApplications = false;
        ShowMassEvents = false;
        ShowMarketingCampaign = true;
        
        StateHasChanged();
    }
    
    public void ShowMassEventsOnScreen()
    {
        ShowLeaseApplications = false;
        ShowMassEvents = true;
        ShowMarketingCampaign = false;
        
        StateHasChanged();
    }
    
    private void EditRequest()
    {
        // Business logic to edit request
    }

    public async Task DeleteLeaseApplication(LeaseApplication leaseApplication)
    {
        await ManageRequestsService.DeleteLeaseApplication(leaseApplication);
    }
    
    private async Task DeleteMarketingCampaign(MarketingCampaign marketingCampaign)
    {
        await ManageRequestsService.DeleteMarketingCampaign(marketingCampaign);
    }
    
    private async Task DeleteMassEvent(MassEvent massEvent)
    {
        await ManageRequestsService.DeleteMassEvent(massEvent);
    }
}