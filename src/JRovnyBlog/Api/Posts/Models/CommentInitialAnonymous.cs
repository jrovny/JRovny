using System.ComponentModel.DataAnnotations;

namespace JRovnyBlog.Api.Posts.Models
{
    public class CommentInitialAnonymous
    {
        public int CommentId { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public string Website { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}