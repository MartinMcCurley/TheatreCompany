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
        public string Description { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Date Posted")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")] // Format as ShortDateTime
        public DateTime DatePosted { get; set; }

        [Display(Name = "Date Expired")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")] // Format as ShortDateTime
        public DateTime DateExpired { get; set; }



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