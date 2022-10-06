using BasicForumApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BasicForumApp.Models;

namespace BasicForumApp.Data
{
    public class BasicForumAppDbContext : DbContext
    {

        public BasicForumAppDbContext
            (DbContextOptions<BasicForumAppDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }
        private Post FirstPost { get; set; } = null!;

        private Post SecondPost { get; set; } = null!;

        private Post ThirdPost { get; set; } = null!;


        public DbSet<Post> Posts { get; init; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedPosts();

            modelBuilder
                .Entity<Post>()
                .HasData(this.FirstPost, this.SecondPost, this.ThirdPost);

            base.OnModelCreating(modelBuilder);
        }


        private void SeedPosts()
        {
            this.FirstPost = new Post
            {
                Id = 1,
                Title = "Nqkvo Zaglavie",
                Content = "Nqkvo Pisanie za purviq post mnogo e tupo."
            };
            this.SecondPost = new Post
            {
                Id = 2,
                Title = "Nqkvo Vtoro Zaglavie",
                Content = "Nqkvo Pisanie za vtoriq post mnogo e tupo."
            };
            this.ThirdPost = new Post
            {
                Id = 3,
                Title = "Nqkvo Treto Zaglavie",
                Content = "Nqkvo Pisanie za tretiq post mnogo e tupo."
            };
        }


        public DbSet<BasicForumApp.Models.PostViewModel> PostViewModel { get; set; }
    }
}