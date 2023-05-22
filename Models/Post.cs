using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
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
        public int PostId { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }


        //===================================================================================
        // Navigational Properties added using "System.ComponentModel.DataAnnotations.Schema"
        //===================================================================================

        // This is the user the post belongs to
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        // This is the category the post belongs to
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // We declare a virtual list of comments 
        public virtual ICollection<Comment> Comments { get; set; }

    }
}