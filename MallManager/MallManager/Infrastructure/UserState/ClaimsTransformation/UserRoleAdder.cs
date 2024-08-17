using System.Security.Claims;
using Ardalis.Specification;
using Microsoft.AspNetCore.Authentication;
using Shared.Core.Entities;
using Shared.Core.Specifications;

namespace MallManager.Infrastructure.UserState.ClaimsTransformation;

public sealed class UserRoleAdder : IClaimsTransformation
{
    private readonly IRepositoryBase<AspNetUser> _userRepository;

    public UserRoleAdder(IRepositoryBase<AspNetUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identities.FirstOrDefault(c => c.IsAuthenticated);
        if (identity is null)
        {
            return principal;
        }

        var userId = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return principal;
        }

        var userSpec = new UserByIdWithRolesSpec(userId.Value);
        var user = await _userRepository.FirstOrDefaultAsync(userSpec);

        if (user is null)
        {
            return principal;
        }

        return new ClaimsPrincipal(identity);
    }
}