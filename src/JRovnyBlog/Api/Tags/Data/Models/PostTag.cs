using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JRovnyBlog.Api.Posts.Data.Models;

namespace JRovnyBlog.Api.Tags.Data.Models
{
    [Table("post_tag")]
    public class PostTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("post_tag_id")]
        public int PostTagId { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        public Post Post { get; set; }
        [Column("tag_id")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
    }
}
