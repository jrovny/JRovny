using System.ComponentModel.DataAnnotations;

namespace JRovnyBlog.Api.Posts.Models
{
    public class CommentInitialAnonymous
    {
        public int CommentId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public string Website { get; set; }
    }
}