using System;
using System.Collections.Generic;

namespace MallManager.Data.Entities;

public partial class MarketingCampaign
{
    public int Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public int MarketingCampaignReachDictId { get; set; }

    public string Description { get; set; } = null!;

    public string SystemAccessAspNetUsersId { get; set; } = null!;

    public bool RegardsInMall { get; set; }

    public bool IsRerun { get; set; }

    public bool OnWeekdays { get; set; }

    public bool OnWeekends { get; set; }

    public int? PersonId { get; set; }

    public int? CompanyId { get; set; }

    public virtual Company? Company { get; set; }

    public virtual MarketingCampaignReachDict MarketingCampaignReachDict { get; set; } = null!;

    public virtual ICollection<MarketingMaterial> MarketingMaterials { get; set; } = new List<MarketingMaterial>();

    public virtual Person? Person { get; set; }

    public virtual SystemAccess SystemAccessAspNetUsers { get; set; } = null!;
}
