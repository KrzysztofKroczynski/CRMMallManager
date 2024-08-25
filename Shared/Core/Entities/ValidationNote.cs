namespace Shared.Core.Entities;

public partial class ValidationNote
{
    public int Id { get; set; }

    public string AuthorId { get; set; } = null!;

    public int ValidationId { get; set; }

    public string Note { get; set; } = null!;

    public virtual AspNetUser Author { get; set; } = null!;

    public virtual Validation Validation { get; set; } = null!;
}