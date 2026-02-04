namespace AwsSocialMedia.Api.Controllers
{
    using AutoMapper;
    using Core.DTOs;
    using Core.Entities;
    using Core.Exceptions;
    using Core.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Response;

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = _postService.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostGetDto>>(posts);

            var response = new ApiResponse<IEnumerable<PostGetDto>>(postsDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPost(id);

            var postDto = _mapper.Map<PostGetDto>(post);

            var response = new ApiResponse<PostGetDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostCreateDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);

            await _postService.InsertPost(post);

            var postCreated = _mapper.Map<PostGetDto>(post);

            var response = new ApiResponse<PostGetDto>(postCreated);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PostUpdateDto postDto)
        {
            if (id != postDto.PostId)
            {
                throw new BusinessException("El ID de la ruta no coincide con el del body");
            }

            var post = _mapper.Map<Post>(postDto);

            var result = await _postService.UpdatePost(post);

            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeletePost(id);
            _ = new ApiResponse<bool>(result);
            return NoContent();
        }
    }
}
