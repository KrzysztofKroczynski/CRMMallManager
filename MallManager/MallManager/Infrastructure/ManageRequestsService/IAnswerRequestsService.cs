using Shared.Core.Entities;

namespace MallManager.Infrastructure.ManageRequestsService;

public interface IAnswerRequestsService
{
    public Task LoadRequestsAsync();
    public Task AcceptLeaseApplication(LeaseApplication leaseApplication);
    public Task DenyLeaseApplication(LeaseApplication leaseApplication);
    public Task AcceptMarketingCampaign(MarketingCampaign marketingCampaign);
    public Task DenyMarketingCampaign(MarketingCampaign marketingCampaign);
    public Task AcceptMassEvent(MassEvent massEvent);
    public Task DenyMassEvent(MassEvent massEvent);

}