namespace Shared.Core.Entities;

public partial class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateTimeAdded { get; set; }

    public string SystemAccessAspNetUsersId { get; set; } = null!;

    public virtual SystemAccess SystemAccessAspNetUsers { get; set; } = null!;
}