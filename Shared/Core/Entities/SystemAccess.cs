namespace Shared.Core.Entities;

public sealed class SystemAccess
{
    public int Id { get; set; }

    public string AspNetUsersId { get; set; } = null!;

    public int SignupStatusDictId { get; set; }

    public DateTime? ValidUntil { get; set; }

    public int SystemDictId { get; set; }

    public string AssignedManagerId { get; set; } = null!;

    public AspNetUser AspNetUsers { get; set; } = null!;

    public Manager AssignedManager { get; set; } = null!;

    public ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();

    public ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public ICollection<MarketingCampaign> MarketingCampaigns { get; set; } = new List<MarketingCampaign>();

    public ICollection<MassEvent> MassEvents { get; set; } = new List<MassEvent>();

    public ICollection<Message> Messages { get; set; } = new List<Message>();

    public SignupStatusDict SignupStatusDict { get; set; } = null!;

    public SystemDict SystemDict { get; set; } = null!;
}