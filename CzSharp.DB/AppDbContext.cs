namespace CzSharp.DB
{
    public class AppDbContext: IdentityDbContext<User, UserRole, int>
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> context)
            : base(context)
        {
        }
    }
}