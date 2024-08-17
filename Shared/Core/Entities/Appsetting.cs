namespace Shared.Core.Entities;

public sealed class Appsetting
{
    public int Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;
}