using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostsService _postsService;

        public PostsController(
            IMapper mapper,
            IPostsService postsService)
        {
            _mapper = mapper;
            _postsService = postsService;
        }

        [HttpGet()]
        public async Task<IEnumerable<Models.PostSummary>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Models.PostSummary>>(
                await _postsService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var post = _mapper.Map<Models.PostView>(
                await _postsService.GetByIdAsync(id));

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlugAsync(string slug)
        {
            var post = _mapper.Map<Models.PostView>(
               await _postsService.GetBySlugAsync(slug));

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody] Models.PostSaveRequest post)
        {
            var data = _mapper.Map<Data.Models.Post>(post);

            await _postsService.CreateAsync(data);
            post.PostId = data.PostId;

            return CreatedAtAction(nameof(GetByIdAsync), new { id = post.PostId }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Models.PostSaveRequest post)
        {
            if (id != post.PostId)
                return BadRequest(new ProblemDetails
                {
                    Title = "ID Mismatch",
                    Detail = "The ID in the route does not match the ID in the model."
                });

            return Ok(await _postsService.UpdateAsync(_mapper.Map<Data.Models.Post>(post)));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchAsync(
            int id,
            [FromBody] JsonPatchDocument<Models.PostSaveRequest> patch)
        {
            if (patch == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "Invalid Request",
                    Detail = "No content found in body of request."
                });

            var post = _mapper.Map<Models.PostSaveRequest>(
                await _postsService.GetByIdAsync(id));

            if (patch == null)
                return NotFound();

            patch.ApplyTo(post, ModelState);

            return Ok(_mapper.Map<Models.PostSaveRequest>(
                await _postsService.UpdateAsync(_mapper.Map<Data.Models.Post>(post))));
        }

        [HttpPost("{id}/upvote")]
        public async Task<IActionResult> UpvoteAsync(int id)
        {
            var post = await _postsService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<Models.PostUpvoteResponse>(await _postsService.UpvoteAsync(id)));
        }

        [HttpPost("{id}/downvote")]
        public async Task<IActionResult> DownvoteAsync(int id)
        {
            var post = await _postsService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<Models.PostUpvoteResponse>(await _postsService.DownvoteAsync(id)));
        }
    }
}
