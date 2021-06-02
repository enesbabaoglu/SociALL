using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ServerApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Repositories.Concrete
{
    public class SociAllContext : IdentityDbContext<User,Role,int>
    {
        public SociAllContext(DbContextOptions<SociAllContext> options):base(options)
        {
            
        }
        public DbSet<Image> Images { get; set; }
      
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserToUser>()
                .HasKey(k => new { k.UserId, k.FollowerId });
            
            builder.Entity<UserToUser>()
                .HasOne(l => l.User)
                .WithMany(f => f.Followers)
                .HasForeignKey( l => l.UserId);
            
            builder.Entity<UserToUser>()
                .HasOne(l => l.Follower)
                .WithMany(f => f.Following)
                .HasForeignKey( l => l.FollowerId);
        }
    }
}

