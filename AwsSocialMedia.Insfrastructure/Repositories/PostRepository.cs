namespace AwsSocialMedia.Insfrastructure.Repositories
{
    using Core.Entities;
    using Core.Interfaces;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PostRepository(AwsSocialMediaDbContext context) : BaseRepository<Post>(context), IPostRepository
    {
        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(p => p.Id == userId).ToListAsync();
        }
    }
}
