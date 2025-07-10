using Microsoft.EntityFrameworkCore;
using MyCornerAPI.Models;
using System.Collections.Generic;

namespace MyCornerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
