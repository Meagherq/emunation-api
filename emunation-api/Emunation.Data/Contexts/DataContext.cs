using Emunation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Emunation.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserGameProfile> UserGameProfiles { get; set; }
        public DbSet<UserSave> UserSaves { get; set; }
        public DbSet<Game> Games { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGameProfile>(entity =>
            {
                entity.HasMany(s => s.Saves)
                .WithOne(g => g.UserGameProfile)
                .HasForeignKey(x => x.UserGameProfileId);

                entity.HasOne(s => s.Game)
                .WithOne(x => x.UserGameProfile)
                .HasForeignKey<UserGameProfile>(x => x.GameId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(s => s.UserGameProfiles)
                .WithOne(g => g.User)
                .HasForeignKey(x => x.UserId);
            });
        }
    }
}