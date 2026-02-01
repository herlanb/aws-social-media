namespace AwsSocialMedia.Api.Controllers
{
    using AutoMapper;
    using Core.DTOs;
    using Core.Entities;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;  

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostGetDto>>(posts);

            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);

            if (post == null)
            {
                return NotFound(new { Message = $"Post con ID {id} no encontrado" });
            }

            var postDto = _mapper.Map<PostGetDto>(post);

            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCreateDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);

            post.Date = DateTime.Now;

            await _postRepository.InsertPost(post);
            return Ok(post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PostUpdateDto postDto)
        {
            if (id != postDto.PostId)
            {
                return BadRequest(new { Message = "El ID de la ruta no coincide con el del body" });
            }

            var post = _mapper.Map<Post>(postDto);

            var result = await _postRepository.UpdatePost(post);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postRepository.DeletePost(id);
            return Ok(result);
        }
    }
}
