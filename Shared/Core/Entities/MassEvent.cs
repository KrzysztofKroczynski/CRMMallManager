namespace Shared.Core.Entities;

public partial class MassEvent
{
    public int Id { get; set; }

    public string Location { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime DatetimeStart { get; set; }

    public DateTime DatetimeEnd { get; set; }

    public bool? IsApproved { get; set; }

    public DateTime DateAdded { get; set; }

    public int SystemAccessId { get; set; }

    public virtual SystemAccess SystemAccess { get; set; } = null!;
}
