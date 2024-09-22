namespace Shared.Core.Entities;

public sealed partial class CompanySize
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string EmploymentLevel { get; set; } = null!;

    public ICollection<Company> Companies { get; set; } = new List<Company>();
}
