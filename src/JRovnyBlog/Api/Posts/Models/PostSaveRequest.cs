using System;
using System.ComponentModel.DataAnnotations;

namespace JRovnyBlog.Api.Posts.Models
{
    public class PostSaveRequest
    {
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string Slug { get; set; }
        public bool Published { get; set; }
        public string Image { get; set; }
    }
}
