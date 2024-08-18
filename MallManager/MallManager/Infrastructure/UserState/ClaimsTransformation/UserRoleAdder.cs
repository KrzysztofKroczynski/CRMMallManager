using System.Security.Claims;
using Ardalis.Specification;
using Microsoft.AspNetCore.Authentication;
using Shared.Core;
using Shared.Core.Entities;
using Shared.Core.Specifications;

namespace MallManager.Infrastructure.UserState.ClaimsTransformation;

public sealed class UserRoleAdder : IClaimsTransformation
{
    private readonly ILogger<UserRoleAdder> _logger;
    private readonly IRepositoryBase<AspNetUser> _userRepository;

    public UserRoleAdder(IRepositoryBase<AspNetUser> userRepository, ILogger<UserRoleAdder> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identities.FirstOrDefault(c => c.IsAuthenticated);
        if (identity is null) return principal;

        var userId = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userId is null) return principal;

        var userSpec = new UserByIdWithRolesSpec(userId.Value);
        var user = await _userRepository.FirstOrDefaultAsync(userSpec);

        if (user is null) return principal;

        if (user.Manager is not null && !principal.HasClaim(claim =>
                claim.Type == ClaimTypes.Role && claim.Value == Roles.RoleClaimValues.Manager.ToString()))
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, Roles.RoleClaimValues.Manager.ToString()));
        }

        foreach (var userSystemAccess in user.SystemAccesses)
        {
            if (!Roles.SystemToRoleClaimValueMap.TryGetValue(userSystemAccess.SystemDict.Name, out var claimValues)
                && principal.HasClaim(claim => claim.Type == ClaimTypes.Role && claim.Value == claimValues.ToString()))
            {
                continue;
            }

            identity.AddClaim(new Claim(ClaimTypes.Role, claimValues.ToString()));
        }

        return new ClaimsPrincipal(identity);
    }
}