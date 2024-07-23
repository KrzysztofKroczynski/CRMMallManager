using System;
using System.Collections.Generic;

namespace MallManager.Data.Entities;

public partial class MarketingCampaignReachDict
{
    public int Id { get; set; }

    public int? MinimalHourlyReach { get; set; }

    public int? MaximalHourlyReach { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<MarketingCampaign> MarketingCampaigns { get; set; } = new List<MarketingCampaign>();
}
