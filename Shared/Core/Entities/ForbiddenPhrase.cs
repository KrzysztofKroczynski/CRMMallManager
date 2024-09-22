namespace Shared.Core.Entities;

public sealed partial class ForbiddenPhrase
{
    public int Id { get; set; }

    public string Phrase { get; set; } = null!;

    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
