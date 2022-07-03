using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Thu_y.Infrastructure.DbContext
{
    public abstract class BaseContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
    {
        protected BaseContext()
        {
            Database.Migrate();
        }

        protected BaseContext(DbContextOptions options)
            : base(options)
        {
            Database.Migrate();
        }
    }
}
