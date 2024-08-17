namespace Shared.Core.Entities;

public sealed class LeaseApplication
{
    public int Id { get; set; }

    public DateOnly DateStart { get; set; }

    public DateOnly DateEnd { get; set; }

    public string Description { get; set; } = null!;

    public int SignupStatusDictId { get; set; }

    public int SystemAccessId { get; set; }

    public SignupStatusDict SignupStatusDict { get; set; } = null!;

    public SystemAccess SystemAccess { get; set; } = null!;

    public ICollection<RetailUnitPurpose> RetailUnitPurposes { get; set; } = new List<RetailUnitPurpose>();

    public ICollection<SurfaceClassDict> SurfaceClassDicts { get; set; } = new List<SurfaceClassDict>();
}