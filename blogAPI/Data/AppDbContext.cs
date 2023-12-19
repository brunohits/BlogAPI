using blogAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace blogAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<PostModel> Posts { get; set; }
        public DbSet<UserPostLikes> Likes { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<CommentsPosts> CommentsPosts { get; set; }
        public DbSet<CommunityModel> Communities { get; set; }
        public DbSet<UsersCommunitiesModel> UsersSubscriptions { get; set; }
        public DbSet<UserCommunityRole> UserCommunityRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasIndex(x => x.fullName);
            modelBuilder.Entity<Token>().HasKey(x => x.InvalidToken);
            modelBuilder.Entity<PostModel>().HasIndex(x => x.id);
            modelBuilder.Entity<UserPostLikes>().HasKey(x => new {x.UserId,x.PostId});
            modelBuilder.Entity<TagModel>().HasIndex(x => x.id);
            modelBuilder.Entity<PostTags>().HasKey(x => new { x.postId, x.tagId });
            modelBuilder.Entity<CommentModel>().HasIndex(x => x.Id);
            modelBuilder.Entity<CommentsPosts>().HasKey(x => new { x.postId,x.commentId });
            modelBuilder.Entity<CommunityModel>().HasIndex(x => x.id);
            modelBuilder.Entity<UsersCommunitiesModel>().HasKey(x => new { x.UserId, x.CommunityId });
            modelBuilder.Entity<UserCommunityRole>().HasKey(x => new { x.UserId, x.CommunityId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
