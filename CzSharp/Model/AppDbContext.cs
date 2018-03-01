using System;
using System.Configuration;
using System.Diagnostics;
using CzSharp.Model.Entities;
using CzSharp.Model.Entities.Blog;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CzSharp.Model
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