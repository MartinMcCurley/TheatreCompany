using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreCompany.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Content")]
        public string Body { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //===================================================================================
        // Navigational Properties added using "System.ComponentModel.DataAnnotations.Schema"
        //===================================================================================

        // This is the user the post belongs to
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        // This is the category the post belongs to
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        // We declare a virtual list of comments 
        public virtual ICollection<Comment> Comments { get; set; }
    }
}