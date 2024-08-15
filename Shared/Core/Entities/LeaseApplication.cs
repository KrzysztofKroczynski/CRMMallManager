namespace Shared.Core.Entities;

public partial class LeaseApplication
{
    public int Id { get; set; }

    public string LeaseAccessAspNetUsersId { get; set; } = null!;

    public DateOnly DateStart { get; set; }

    public DateOnly DateEnd { get; set; }

    public string Description { get; set; } = null!;

    public int SignupStatusDictId { get; set; }

    public virtual SystemAccess LeaseAccessAspNetUsers { get; set; } = null!;

    public virtual SignupStatusDict SignupStatusDict { get; set; } = null!;

    public virtual ICollection<RetailUnitPurpose> RetailUnitPurposes { get; set; } = new List<RetailUnitPurpose>();

    public virtual ICollection<SurfaceClassDict> SurfaceClassDicts { get; set; } = new List<SurfaceClassDict>();
}