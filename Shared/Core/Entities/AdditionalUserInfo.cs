namespace Shared.Core.Entities;

public partial class AdditionalUserInfo
{
    public string AspNetUsersId { get; set; } = null!;

    public byte[]? UserPhoto { get; set; }

    public virtual AspNetUser AspNetUsers { get; set; } = null!;
}
