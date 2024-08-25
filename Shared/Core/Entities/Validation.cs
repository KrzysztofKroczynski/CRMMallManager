namespace Shared.Core.Entities;

public partial class Validation
{
    public int Id { get; set; }

    public bool IsValid { get; set; }

    public string LastChangedById { get; set; } = null!;

    public DateTime LastChangeDate { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual AspNetUser LastChangedBy { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    public virtual ICollection<ValidationNote> ValidationNotes { get; set; } = new List<ValidationNote>();
}