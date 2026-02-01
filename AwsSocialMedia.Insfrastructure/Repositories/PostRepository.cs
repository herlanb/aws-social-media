namespace AwsSocialMedia.Insfrastructure.Repositories
{
    using Data;
    using Core.Entities;
    using Core.Interfaces;
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
    }
}
