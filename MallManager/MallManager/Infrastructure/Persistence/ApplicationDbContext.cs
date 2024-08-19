using MallManager.Infrastructure.Configuration.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MallManager.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    private readonly IOptions<DbConnectionString>? _connStringOptions;


    public ApplicationDbContext(IOptions<DbConnectionString>? connStringOptions,
        DbContextOptions<ApplicationDbContext> options)
        : this(options)
    {
        _connStringOptions = connStringOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(_connStringOptions?.Value?.DefaultConnection ?? string.Empty);
    }
}