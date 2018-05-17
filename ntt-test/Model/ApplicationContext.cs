using System.Data.Entity;

namespace ntt_test.Model
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        { }
        public DbSet<Link> Links { get; set; }
    }
}