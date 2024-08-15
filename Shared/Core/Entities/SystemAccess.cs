namespace Shared.Core.Entities;

public partial class SystemAccess
{
    public string AspNetUsersId { get; set; } = null!;

    public int SignupStatusDictId { get; set; }

    public DateTime? ValidUntil { get; set; }

    public int SystemDictId { get; set; }

    public string AssignedManagerId { get; set; } = null!;

    public virtual AspNetUser AspNetUsers { get; set; } = null!;

    public virtual Manager AssignedManager { get; set; } = null!;

    public virtual ICollection<LeaseApplication> LeaseApplications { get; set; } = new List<LeaseApplication>();

    public virtual ICollection<Lease> Leases { get; set; } = new List<Lease>();

    public virtual ICollection<MarketingCampaign> MarketingCampaigns { get; set; } = new List<MarketingCampaign>();

    public virtual ICollection<MassEvent> MassEvents { get; set; } = new List<MassEvent>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual SignupStatusDict SignupStatusDict { get; set; } = null!;

    public virtual SystemDict SystemDict { get; set; } = null!;
}