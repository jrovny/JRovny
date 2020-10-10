using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JRovnyBlog.Api.Images.Data.Models
{
    [Table("image")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("image_id")]
        public int ImageId { get; set; }
        [Column("original_file_name")]
        public string OriginalFileName { get; set; }
        [Column("file_name")]
        public string FileName { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
    }
}
