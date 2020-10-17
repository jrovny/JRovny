using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JRovnyBlog.Api.Posts.Data.Models;

namespace JRovnyBlog.Api.Tags.Data.Models
{
    [Table("tag")]
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("tag_id")]
        public int TagId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
        [Column("deleted")]
        public bool Deleted { get; set; }
        //public ICollection<Post> Posts { get; set; }
        public List<PostTag> PostTags { get; set; }

        public Tag()
        {
            //Posts = new Collection<Post>();
            PostTags = new List<PostTag>();
        }
    }
}
