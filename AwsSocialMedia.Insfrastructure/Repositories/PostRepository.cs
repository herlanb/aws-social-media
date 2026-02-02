namespace AwsSocialMedia.Insfrastructure.Repositories
{
    using Core.Entities;
    using Core.Interfaces;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PostRepository : IPostRepository
    {
        private readonly AwsSocialMediaDbContext _context;

        public PostRepository(AwsSocialMediaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .ToListAsync();
        }

        public async Task<Post> GetPost(int id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments
                        .OrderByDescending(c => c.Date)
                        .Take(20))
                    .ThenInclude(c => c.User)
                .AsNoTracking()
                .AsSplitQuery()
                .FirstOrDefaultAsync(p => p.PostId == id);
        }

        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var currentPost = await GetPost(post.PostId);
            currentPost.Description = post.Description;
            currentPost.Image = post.Image;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeletePost(int id)
        {
            var currentPost = await GetPost(id);

            _context.Posts.Remove(currentPost);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }
}
