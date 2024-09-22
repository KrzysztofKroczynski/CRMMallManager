using Ardalis.Specification.EntityFrameworkCore;

namespace MallManager.Infrastructure.Persistence.Repositories;

public sealed class Repository<T> : RepositoryBase<T> where T : class
{
    public Repository(MallManagerContext dbContext) : base(dbContext)
    {
    }
}