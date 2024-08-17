namespace Shared.Core.Entities;

public sealed class MarketingMaterial
{
    public int Id { get; set; }

    public int MarketingCampaignId { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Content { get; set; } = null!;

    public decimal? PriceFactor { get; set; }

    public MarketingCampaign MarketingCampaign { get; set; } = null!;
}