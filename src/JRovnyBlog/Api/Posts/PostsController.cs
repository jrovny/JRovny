using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Posts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PostsController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet()]
        public async Task<IEnumerable<Models.PostSummary>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Models.PostSummary>>(
                await _context.Posts.AsNoTracking().OrderByDescending(p => p.PostId).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var post = _mapper.Map<Models.PostView>(
                await _context.Posts
                    .AsNoTracking()
                    .Where(p => p.PostId == id)
                    .FirstOrDefaultAsync());

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlugAsync(string slug)
        {
            var post = _mapper.Map<Models.PostView>(
                await _context.Posts
                    .AsNoTracking()
                    .Where(p => p.Slug == slug)
                    .FirstOrDefaultAsync());

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody] Models.PostSaveRequest post)
        {
            var data = _mapper.Map<Data.Models.Post>(post);

            _context.Posts.Add(data);
            await _context.SaveChangesAsync();

            post.PostId = data.PostId;

            return CreatedAtAction(nameof(GetByIdAsync), post);
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

            var data = _mapper.Map<Data.Models.Post>(post);
            _context.Update(data);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        [HttpPost("{id}/upvote")]
        public async Task<IActionResult> UpvoteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            post.UpvoteCount += 1;

            await _context.SaveChangesAsync();

            return Ok(new Models.PostUpvoteResponse { UpvoteCount = post.UpvoteCount });
        }

        [HttpPost("{id}/downvote")]
        public async Task<IActionResult> DownvoteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return NotFound();

            post.UpvoteCount -= 1;

            if (post.UpvoteCount < 0)
                post.UpvoteCount = 0;

            await _context.SaveChangesAsync();

            return Ok(new Models.PostUpvoteResponse { UpvoteCount = post.UpvoteCount });
        }
    }
}
