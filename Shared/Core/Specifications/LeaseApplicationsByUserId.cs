using Ardalis.Specification;
using Shared.Core.Entities;

namespace Shared.Core.Specifications;

public class LeaseApplicationsByUserId : Specification<LeaseApplication>
{
    public LeaseApplicationsByUserId(string userId)
    {
        Query
            .Where(lease => lease.SystemAccess.AspNetUsers.Id == userId)
            .Include(lease => lease.SignupStatusDict);
    }
}