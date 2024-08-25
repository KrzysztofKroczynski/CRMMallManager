namespace Shared.Core.Entities;

public sealed class Company
{
    public int Id { get; set; }

    public string? AspNetUsersId { get; set; }

    public string Name { get; set; } = null!;

    public string Nip { get; set; } = null!;

    public string Regon { get; set; } = null!;

    public decimal StartingCapital { get; set; }

    public int CompanySizeId { get; set; }

    public DateTime DateAdded { get; set; }

    public int ValidationId { get; set; }

    public AspNetUser? AspNetUsers { get; set; }

    public CompanySize CompanySize { get; set; } = null!;

    public ICollection<MarketingCampaign> MarketingCampaigns { get; set; } = new List<MarketingCampaign>();

    public Validation Validation { get; set; } = null!;
}