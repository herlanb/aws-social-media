namespace AwsSocialMedia.Core.Services
{
    using Exceptions;
    using Entities;
    using Interfaces;
    using System.Collections.Generic;

    public class PostService(IUnitOfWork unitOfWork) : IPostService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IEnumerable<Post> GetPosts()
        {
            return _unitOfWork.PostRepository.GetAll();
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _unitOfWork.PostRepository.GetById(id);
            
            return post ?? throw new BusinessException($"El post con ID {id} no existe");
        }

        public async Task InsertPost(Post post)
        {
            _ = await _unitOfWork.UserRepository.GetById(post.UserId)
                   ?? throw new BusinessException("El usuario no existe.");

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);

            var postLastWeek = userPost
                .Where(p => (DateTime.Now - p.Date).TotalDays <= 7)
                .Count();
            if (postLastWeek >= 10)
            {
                throw new BusinessException("Has alcanzado el límite de 10 publicaciones por semana");
            }

            var lastPost = userPost.OrderByDescending(p => p.Date).FirstOrDefault();
            if (lastPost != null && (DateTime.Now - lastPost.Date).TotalDays < 1) 
            {
                throw new BusinessException("Debes esperar al menos un día para pueblicar un nuevos post");
            }
            
            if (post.Description.Contains("sexo", StringComparison.OrdinalIgnoreCase))
            {
                throw new BusinessException("No se permite este tipo de post.");
            }

            post.Date = DateTime.Now;

            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var existinPost = await GetPost(post.Id);

            existinPost.Description = post.Description;
            existinPost.Image = post.Image;

            _unitOfWork.PostRepository.Update(existinPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            var post = await GetPost(id);

            await _unitOfWork.PostRepository.Delete(post.Id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
