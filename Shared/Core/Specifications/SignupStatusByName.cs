using Ardalis.Specification;
using Shared.Core.Entities;

namespace Shared.Core.Specifications;

public class SignupStatusByName : Specification<SignupStatusDict>
{
    public SignupStatusByName(string name)
    {
        Query
            .Where(signupStatusDict => signupStatusDict.Name == name);
    }
}