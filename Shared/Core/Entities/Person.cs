﻿namespace Shared.Core.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string? AspNetUsersId { get; set; }

    public string Name { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Pesel { get; set; } = null!;

    public string? SecondName { get; set; }

    public DateTime DateAdded { get; set; }

    public int ValidationId { get; set; }

    public virtual AspNetUser? AspNetUsers { get; set; }

    public virtual ICollection<MarketingCampaign> MarketingCampaigns { get; set; } = new List<MarketingCampaign>();

    public virtual Validation Validation { get; set; } = null!;
}
