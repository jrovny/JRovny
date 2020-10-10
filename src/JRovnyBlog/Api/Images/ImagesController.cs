using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace JRovnyBlog.Api.Images
{
    [Route("api/posts/{postId}/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IImagesService _imagesService;
        private readonly IMapper _mapper;

        public ImagesController(
            IHostEnvironment hostEnvironment,
            IImagesService imagesService,
            IMapper mapper)
        {
            _hostEnvironment = hostEnvironment;
            _imagesService = imagesService;
            this._mapper = mapper;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(
            IFormFile file,
            CancellationToken cancellationToken = default)
        {
            if (file == null)
                return BadRequest(new ProblemDetails
                {
                    Title = "Error Occurred",
                    Detail = "File is null."
                });

            if (file.Length == 0)
                return BadRequest(new ProblemDetails
                {
                    Title = "Error Occurred",
                    Detail = "File is empty."
                });

            var folderPath = Path.Combine($"{_hostEnvironment.ContentRootPath}", "wwwroot", "images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName).ToLower()}";

            using (var stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return Ok(_mapper.Map<Models.Image>(
                await _imagesService.CreateAsync(new Data.Models.Image
            {
                OriginalFileName = file.FileName,
                FileName = fileName
            })));
        }
    }
}
