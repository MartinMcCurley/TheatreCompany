// Author: Martin McCurley | Date: 04/02/22
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
        public string Body { get; set; }

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