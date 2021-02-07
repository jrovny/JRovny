using JRovnyBlog.Api.Posts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Tags
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly ILogger<TagsController> _logger;

        public TagsController(IPostsService postsService, ILogger<TagsController> logger)
        {
            _postsService = postsService;
            _logger = logger;
        }

        [
        HttpGet("{id}/posts")]
        public async Task<IActionResult> GetAllPostsByTagIdAsync(int id)
        {
            _logger.LogInformation("Getting all blog posts by tag ID {id}", id);
            return Ok(await _postsService.GetAllByTagAsync(id));
        }
    }
}
