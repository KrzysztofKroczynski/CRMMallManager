namespace Shared.Core.Entities;

public partial class Message
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public DateTime DateTimeAdded { get; set; }

    public int SystemAccessId { get; set; }

    public string AspNetUsersId { get; set; } = null!;

    public virtual AspNetUser AspNetUsers { get; set; } = null!;

    public virtual SystemAccess SystemAccess { get; set; } = null!;

    public virtual ICollection<ForbiddenPhrase> ForbiddenPhrases { get; set; } = new List<ForbiddenPhrase>();
}
