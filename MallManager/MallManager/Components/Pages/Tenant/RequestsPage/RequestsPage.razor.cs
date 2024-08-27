using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared.Core.Entities;
using Shared.UseCases.GetUserState;

namespace MallManager.Components.Pages.Tenant.RequestsPage;

public partial class RequestsPage : ComponentBase
{
    private ICollection<LeaseApplication> _leaseApplications = new List<LeaseApplication>();
    private ICollection<MarketingCampaign> _marketingCampaigns = new List<MarketingCampaign>();
    private ICollection<MassEvent> _massEvents = new List<MassEvent>();
    public bool ShowLeaseApplications { get; set; } = true;
    public bool ShowMassEvents { get; set; }
    public bool ShowMarketingCampaign { get; set; }

    [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

    private UserState<LeaseApplication> StateEditLeaseApplication { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity.IsAuthenticated)
        {
            NavigationManager.NavigateTo("/Login");
            return;
        }

        var userIdString = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            NavigationManager.NavigateTo("/Login");
            return;
        }

        // var userId = int.Parse(userIdString);
        await ManageRequestsService.LoadAspNetUserRequestsAsync(userIdString);

        _leaseApplications = ManageRequestsService.LeaseApplications;
        _massEvents = ManageRequestsService.MassEvents;
        _marketingCampaigns = ManageRequestsService.MarketingCampaigns;
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

    private async Task EditLeaseApplication(int leaseApplicationId)
    {
        throw new NotImplementedException("This functionality is not implemented yet");
        StateEditLeaseApplication =
            await Mediator.Send(new GetEditedLeaseApplicationStateQuery(leaseApplicationId + ""));
        NavigationManager.NavigateTo($"/Requests/EditLeaseApplication/{leaseApplicationId}");
    }

    public async Task DeleteLeaseApplication(LeaseApplication leaseApplication)
    {
        await ManageRequestsService.DeleteLeaseApplication(leaseApplication);
        StateHasChanged();
    }

    private async Task DeleteMarketingCampaign(MarketingCampaign marketingCampaign)
    {
        await ManageRequestsService.DeleteMarketingCampaign(marketingCampaign);
        StateHasChanged();
    }

    private async Task DeleteMassEvent(MassEvent massEvent)
    {
        await ManageRequestsService.DeleteMassEvent(massEvent);
        StateHasChanged();
    }
}