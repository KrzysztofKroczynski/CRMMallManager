using System;
using System.Collections.Generic;

namespace MallManager.Data.Entities;

public partial class Company
{
    public int Id { get; set; }

    public string? AspNetUsersId { get; set; }

    public string Name { get; set; } = null!;

    public string Nip { get; set; } = null!;

    public string Regon { get; set; } = null!;

    public decimal StartingCapital { get; set; }

    public int CompanySizeId { get; set; }

    public virtual AspNetUser? AspNetUsers { get; set; }

    public virtual CompanySize CompanySize { get; set; } = null!;

    public virtual ICollection<MarketingCampaign> MarketingCampaigns { get; set; } = new List<MarketingCampaign>();
}
