using Microsoft.EntityFrameworkCore;
using Autod.Core.Domain;

namespace Autod.Data
{
    public class AutodDbContext : DbContext
    {
        public AutodDbContext(DbContextOptions<AutodDbContext> options)
            : base(options) { }

        public DbSet<Auto> Auto { get; set; }
        public DbSet<ExistingFilePath> ExistingFilePath { get; set; }
    }
}
