﻿namespace Shared.Core.Entities;

public partial class MarketingCampaign
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

    public virtual Company? Company { get; set; }

    public virtual MarketingCampaignReachDict MarketingCampaignReachDict { get; set; } = null!;

    public virtual ICollection<MarketingMaterial> MarketingMaterials { get; set; } = new List<MarketingMaterial>();

    public virtual Person? Person { get; set; }

    public virtual SystemAccess SystemAccess { get; set; } = null!;
}
