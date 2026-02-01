namespace AwsSocialMedia.Core.Interfaces
{
    using Entities;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPost(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}
