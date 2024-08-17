namespace Shared.Core.Entities;

public sealed class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public AdditionalUserInfo? AdditionalUserInfo { get; set; }

    public ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public ICollection<Company> Companies { get; set; } = new List<Company>();

    public Manager? Manager { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();

    public ICollection<Person> People { get; set; } = new List<Person>();

    public ICollection<SystemAccess> SystemAccesses { get; set; } = new List<SystemAccess>();

    public ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}