
using CzSharp.Model.Entities;
using CzSharp.Model.Entities.Blog;
using CzSharp.Model.Entities.Forum;
using Microsoft.AspNetCore.Identity;
 using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.Model
{
    public class AppDbContext: IdentityDbContext<User, UserRole, int>
    {
        /// <summary>
        /// Articles table
        /// </summary>
        public DbSet<Article> Articles { get; set; }
        /// <summary>
        /// Categories table
        /// </summary>
        public DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Codes table
        /// </summary>
        public DbSet<Code> Codes { get; set; }
        /// <summary>
        /// Contributions table
        /// </summary>
        public DbSet<Contribution> Contributions { get; set; }
        /// <summary>
        /// Discussions table
        /// </summary>
        public DbSet<Discussion> Discussions { get; set; }
        /// <summary>
        /// Events table
        /// </summary>
        public DbSet<Event> Events { get; set; }
        /// <summary>
        /// Tags table
        /// </summary>
        public DbSet<Tag> Tags { get; set; }
        /// <summary>
        /// Topics table
        /// </summary>
        public DbSet<Topic> Topics { get; set; }
        /// <summary>
        /// Topic groups table
        /// </summary>
        public DbSet<TopicGroup> TopicGroups { get; set; }
        /// <summary>
        /// Comments table
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> context)
            : base(context)
        {
        }

        /// <summary>
        /// Creating model with many-to-many relationships
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ArticleTag>().HasKey(t => new {t.ArticleId, t.TagId});
            builder.Entity<ArticleTag>().HasOne(at => at.Article).WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleId);
            builder.Entity<ArticleTag>().HasOne(at => at.Tag).WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagId);
            
            builder.Entity<CodeTag>().HasKey(t => new {t.CodeId, t.TagId});
            builder.Entity<CodeTag>().HasOne(ct => ct.Code).WithMany(c => c.CodeTags)
                .HasForeignKey(ct => ct.CodeId);
            builder.Entity<CodeTag>().HasOne(ct => ct.Tag).WithMany(t => t.CodeTags)
                .HasForeignKey(ct => ct.TagId);

            builder.Entity<DiscussionTag>().HasKey(t => new {t.DiscussionId, t.TagId});
            builder.Entity<DiscussionTag>().HasOne(dt => dt.Discussion).WithMany(d => d.DiscussionTags)
                .HasForeignKey(dt => dt.DiscussionId);
            builder.Entity<DiscussionTag>().HasOne(dt => dt.Tag).WithMany(t => t.DiscussionTags)
                .HasForeignKey(dt => dt.TagId);
            
            builder.Entity<EventTag>().HasKey(t => new {t.EventId, t.TagId});
            builder.Entity<EventTag>().HasOne(et => et.Event).WithMany(e => e.EventTags)
                .HasForeignKey(et => et.EventId);
            builder.Entity<EventTag>().HasOne(et => et.Tag).WithMany(t => t.EventTags)
                .HasForeignKey(et => et.TagId);

            builder.Entity<TopicGroup>().HasIndex(t => t.Title).IsUnique();
        }
    }
}