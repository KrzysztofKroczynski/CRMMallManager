using Ardalis.Specification.EntityFrameworkCore;

namespace MallManager.Infrastructure.Persistence;

public sealed class Repository<T> : RepositoryBase<T> where T : class
{
    public Repository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}