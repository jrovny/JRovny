using AutoMapper;
using JRovnyBlog.Api.Posts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPostsService _postsService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(
            IMapper mapper,
            IPostsService postsService,
            ILogger<PostsController> logger)
        {
            _mapper = mapper;
            _postsService = postsService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IEnumerable<Models.PostSummary>> GetAllAsync()
        {
            _logger.LogInformation("Getting all blog posts");

            return _mapper.Map<IEnumerable<Models.PostSummary>>(
                await _postsService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting blog post by id");

            var post = _mapper.Map<Models.PostView>(
                await _postsService.GetByIdAsync(id));

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlugAsync(string slug)
        {
            _logger.LogInformation("Getting blog post by slug");

            var post = _mapper.Map<Models.PostView>(
               await _postsService.GetBySlugAsync(slug));

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody] Models.PostSaveRequest post)
        {
            _logger.LogInformation("Creating blog post {@post}", post);

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

            _logger.LogInformation("Updating blog post {@post}", post);

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

            _logger.LogInformation("Updating blog post {@post}", patch);

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
            _logger.LogInformation("Upvoting blog post");

            var post = await _postsService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<Models.PostVoteResponse>(await _postsService.UpvoteAsync(id)));
        }

        [HttpPost("{id}/downvote")]
        public async Task<IActionResult> DownvoteAsync(int id)
        {
            _logger.LogInformation("Downvoting blog post");

            var post = await _postsService.GetByIdAsync(id);

            if (post == null)
                return NotFound();

            return Ok(_mapper.Map<Models.PostVoteResponse>(await _postsService.DownvoteAsync(id)));
        }

        [HttpPost("{id}/comment-initial-anon")]
        public async Task<IActionResult> CreateInitialCommentAnonymousAsync(
            int id,
            CommentInitialAnonymous initComment)
        {
            _logger.LogInformation("Creating initial comment for anonymous user");

            var proxyIp = Request.Headers["X-Forwarded-For"].FirstOrDefault();
            var comment = _mapper.Map<Data.Models.Comment>(initComment);
            comment.PostId = id;
            comment.UserIp = proxyIp != null
                ? proxyIp.ToString()
                : Request.HttpContext.Connection.RemoteIpAddress.ToString();
            comment.IsAnonymous = true;
            comment.UserAgent = Request.Headers[HeaderNames.UserAgent];

            await _postsService.CreateInitialCommentAsync(comment);
            initComment.CommentId = comment.CommentId;

            return CreatedAtAction(nameof(GetByIdAsync), new { id = initComment.CommentId }, initComment);
        }
    }
}
