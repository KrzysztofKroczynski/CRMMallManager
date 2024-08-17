namespace Shared.Core.Entities;

public sealed class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateTimeAdded { get; set; }

    public int SystemAccessId { get; set; }

    public string AspNetUsersId { get; set; } = null!;

    public AspNetUser AspNetUsers { get; set; } = null!;

    public SystemAccess SystemAccess { get; set; } = null!;
}