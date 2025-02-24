using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreCompany.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string Body { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //===================================================================================
        // Navigational Properties added using "System.ComponentModel.DataAnnotations.Schema"
        //===================================================================================

        // This is the user the comment belongs to
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // This is the post the comment belongs to
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}