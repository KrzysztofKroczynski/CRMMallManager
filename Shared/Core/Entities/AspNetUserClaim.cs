namespace Shared.Core.Entities;

public sealed class AspNetUserClaim
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public string? ClaimType { get; set; }

    public string? ClaimValue { get; set; }

    public AspNetUser User { get; set; } = null!;
}