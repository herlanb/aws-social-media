namespace AwsSocialMedia.Api.Controllers
{
    using AwsSocialMedia.Core.Entities;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Hosting;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);

            return Ok(post);
        }
    }
}
