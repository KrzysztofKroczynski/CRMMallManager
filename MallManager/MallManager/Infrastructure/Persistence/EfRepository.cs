using MallManager.Data;

namespace MallManager.Infrastructure.Persistence;

using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;



public class EfRepository<T>(ApplicationDbContext dbContext) :
    RepositoryBase<T>(dbContext), IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
}