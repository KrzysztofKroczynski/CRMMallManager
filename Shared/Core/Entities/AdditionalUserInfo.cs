namespace Shared.Core.Entities;

public sealed class AdditionalUserInfo
{
    public string AspNetUsersId { get; set; } = null!;

    public byte[] UserPhoto { get; set; } = null!;

    public AspNetUser AspNetUsers { get; set; } = null!;
}