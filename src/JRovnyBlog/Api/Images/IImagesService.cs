using JRovnyBlog.Api.Images.Data.Models;
using System.Threading.Tasks;

namespace JRovnyBlog.Api.Images
{
    public interface IImagesService
    {
        Task<Image> CreateAsync(Image image);
    }
}