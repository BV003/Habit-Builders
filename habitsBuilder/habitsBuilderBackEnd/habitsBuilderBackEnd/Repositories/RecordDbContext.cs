using habitsBuilderBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace habitsBuilderBackEnd.Repositories
{
    public class RecordDbContext : DbContext
    {
        public RecordDbContext(DbContextOptions<RecordDbContext> options)
           : base(options)
        {
            this.Database.EnsureCreated(); //自动建库建表
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostPhoto> PostPhotos { get; set; }
        public DbSet<HabitCard> HabitCards { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFriend>()
           .HasKey(uf => new { uf.UserId, uf.FriendId });

            modelBuilder.Entity<UserFriend>()
                .HasOne<User>()
                .WithMany(u => u.Friends)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFriend>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(uf => uf.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Post>()
                .HasKey(p => p.PostId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User) // Assuming Post entity has a User navigation property
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostPhoto>()
                .HasKey(pp => pp.PostPhotoId);

            modelBuilder.Entity<PostPhoto>()
                .HasOne(pp => pp.Post)
                .WithMany(p => p.Photos)
                .HasForeignKey(pp => pp.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLike>()
                .HasKey(pl => pl.PostLikeId);

            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.Post)
                .WithMany(p => p.Likes)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostLike>()
                .HasOne(pl => pl.User)
                .WithMany()
                .HasForeignKey(pl => pl.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

            modelBuilder.Entity<HabitCard>()
                .HasOne(h => h.User)
                .WithMany(u => u.HabitCards)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<ChecklistItem>()
                .HasOne(c => c.Card)
                .WithMany(h => h.ChecklistItems)
                .HasForeignKey(c => c.CardId);
        }

    }
}