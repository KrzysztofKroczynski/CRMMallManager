using Ardalis.Specification;
using Shared.Core.Entities;

namespace Shared.Core.Specifications;

public class MarketingCampaignsByUserId : Specification<MarketingCampaign>
{
    public MarketingCampaignsByUserId(string userId)
    {
        Query
            .Where(marketingCampaign => marketingCampaign.SystemAccess.AspNetUsers.Id == userId)
            .Include(marketingCampaign => marketingCampaign.MarketingCampaignReachDict);
    }
}