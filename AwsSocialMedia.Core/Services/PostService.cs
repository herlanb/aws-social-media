namespace AwsSocialMedia.Core.Services
{
    using Entities;
    using Interfaces;
    using System.Collections.Generic;

    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        public Task<Post> GetPost(int id)
        {
            return _postRepository.GetPost(id);
        }

        public async Task InsertPost(Post post)
        {
            _ = await _userRepository.GetUser(post.UserId)
                   ?? throw new Exception("El usuario no existe.");

            if (post.Description.Contains("sexo"))
            {
                throw new Exception("No se permite este tipo de post.");
            }

            post.Date = DateTime.Now;

            await _postRepository.InsertPost(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            return await _postRepository.UpdatePost(post);
        }

        public Task<bool> DeletePost(int id)
        {
            return _postRepository.DeletePost(id);
        }
    }
}
