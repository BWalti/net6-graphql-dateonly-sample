using Microsoft.EntityFrameworkCore;

namespace graphql_minimal_api
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=Demo.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .Property(p => p.Birthday)
                .HasConversion(
                    v => v.ToDateTime(new TimeOnly(0, 0)),
                    v => DateOnly.FromDateTime(v));
        }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }
    }
}
