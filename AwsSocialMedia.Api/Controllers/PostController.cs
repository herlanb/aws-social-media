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
    }
}
