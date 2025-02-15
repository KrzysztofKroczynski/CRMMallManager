﻿using Shared.Core.Entities;

namespace MallManager.Infrastructure.ManageRequestsService;

public interface IManageRequestsService
{
    public Task LoadAspNetUserRequestsAsync(string userId);
    public Task DeleteLeaseApplication(LeaseApplication leaseApplication);
    public Task DeleteMarketingCampaign(MarketingCampaign marketingCampaign);
    public Task DeleteMassEvent(MassEvent massEvent);
}