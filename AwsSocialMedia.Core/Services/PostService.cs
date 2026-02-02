namespace AwsSocialMedia.Core.Services
{
    using Entities;
    using Interfaces;
    using System.Collections.Generic;

    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<Post>> GetPosts()
        {
            return _unitOfWork.PostRepository.GetAll();
        }

        public Task<Post> GetPost(int id)
        {
            return _unitOfWork.PostRepository.GetById(id);
        }

        public async Task InsertPost(Post post)
        {
            _ = await _unitOfWork.UserRepository.GetById(post.UserId)
                   ?? throw new Exception("El usuario no existe.");

            if (post.Description.Contains("sexo"))
            {
                throw new Exception("No se permite este tipo de post.");
            }

            post.Date = DateTime.Now;

            await _unitOfWork.PostRepository.Add(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _unitOfWork.PostRepository.Update(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
