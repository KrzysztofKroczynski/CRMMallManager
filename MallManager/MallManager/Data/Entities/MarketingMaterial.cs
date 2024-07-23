using System;
using System.Collections.Generic;

namespace MallManager.Data.Entities;

public partial class MarketingMaterial
{
    public int Id { get; set; }

    public int MarketingCampaignId { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Content { get; set; } = null!;

    public decimal? PriceFactor { get; set; }

    public virtual MarketingCampaign MarketingCampaign { get; set; } = null!;
}
