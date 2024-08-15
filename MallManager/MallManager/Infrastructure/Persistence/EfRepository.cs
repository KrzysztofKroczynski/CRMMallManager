using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace MallManager.Infrastructure.Persistence;

public class EfRepository<T>(ApplicationDbContext dbContext) :
    RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}