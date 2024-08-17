using Ardalis.Specification;
using Shared.Core.Entities;

namespace Shared.Core.Specifications;

public sealed class UserByIdWithRolesSpec : Specification<AspNetUser>
{
    public UserByIdWithRolesSpec(string userId)
    {
        Query
            .Where(user => user.Id == userId);
    }
}