using Microsoft.EntityFrameworkCore;

namespace CodingAssessment.Persistence.Postgre
{
    internal sealed class PostgreContext : DbContext
    {
        public DbSet<PostgreAgent> Agents { get; set; } = null!;

        public PostgreContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {

        }
    }
}
