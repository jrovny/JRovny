using System;
using System.Collections.Generic;

namespace JRovnyBlog.Api.Posts.Models
{
    public class CommentView
    {
        public int CommentId { get; set; }
        public int ParentCommentId { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<CommentView> Children { get; set; }
    }
}
