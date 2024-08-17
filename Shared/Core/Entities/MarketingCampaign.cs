namespace Shared.Core.Entities;

public sealed class MarketingCampaign
{
    public int Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int MarketingCampaignReachDictId { get; set; }

    public string Description { get; set; } = null!;

    public bool RegardsInMall { get; set; }

    public bool IsRerun { get; set; }

    public bool OnWeekdays { get; set; }

    public bool OnWeekends { get; set; }

    public int? PersonId { get; set; }

    public int? CompanyId { get; set; }

    public int SystemAccessId { get; set; }

    public Company? Company { get; set; }

    public MarketingCampaignReachDict MarketingCampaignReachDict { get; set; } = null!;

    public ICollection<MarketingMaterial> MarketingMaterials { get; set; } = new List<MarketingMaterial>();

    public Person? Person { get; set; }

    public SystemAccess SystemAccess { get; set; } = null!;
}