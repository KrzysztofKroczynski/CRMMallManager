namespace Shared.Core;

public static class Roles
{
    public enum RoleClaimValues
    {
        Admin,
        Manager,
        Renter,
        EventOrganiser,
        AddCampaigner
    }

    public static readonly Dictionary<string, RoleClaimValues> SystemToRoleClaimValueMap =
        new()
        {
            { "Wynajmy", RoleClaimValues.Renter },
            { "Imprezy masowe", RoleClaimValues.EventOrganiser },
            { "Kampanie Reklamowe", RoleClaimValues.AddCampaigner }
        };
}