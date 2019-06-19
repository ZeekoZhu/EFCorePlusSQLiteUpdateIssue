using Microsoft.EntityFrameworkCore;

namespace EFCorePlusSQLiteUpdateIssue
{
    public class BlogPost
    {
        public int Id { get; set; }
        public int ViewCount { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, ViewCount: {ViewCount}";
        }
    }

    public class FooDbContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=./foo.db");
        }
    }
}