namespace AwsSocialMedia.Insfrastructure.Data
{
    using Configurations;
    using Core.Entities;
    using Microsoft.EntityFrameworkCore;

    public partial class AwsSocialMediaDbContext : DbContext
    {
        public AwsSocialMediaDbContext()
        {
        }

        public AwsSocialMediaDbContext(DbContextOptions<AwsSocialMediaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CommentConfiguration());

            modelBuilder.ApplyConfiguration(new PostConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}