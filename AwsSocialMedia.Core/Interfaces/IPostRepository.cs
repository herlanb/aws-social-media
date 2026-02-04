namespace AwsSocialMedia.Core.Interfaces
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByUser(int userId);
    }
}
