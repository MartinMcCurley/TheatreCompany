using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TheatreCompany.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

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

        // This is the user the comment belongs to
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        // This is the post the comment belongs to
        [ForeignKey("Post")]
        public int PostId { get; set; }
        public Post Post { get; set; }


    }
}