using Ardalis.Specification;
using Shared.Core.Entities;

namespace Shared.Core.Specifications;

public class MassEventsByUserId : Specification<MassEvent>
{
    public MassEventsByUserId(string userId)
    {
        Query
            .Where(massEvent => massEvent.SystemAccess.AspNetUsers.Id == userId);
    }
}