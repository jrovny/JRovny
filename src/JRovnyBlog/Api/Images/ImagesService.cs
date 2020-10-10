using System.Threading.Tasks;

namespace JRovnyBlog.Api.Images
{
    public class ImagesService : IImagesService
    {
        private readonly ApplicationDbContext _context;

        public ImagesService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Data.Models.Image> CreateAsync(Data.Models.Image image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return image;
        }
    }
}
