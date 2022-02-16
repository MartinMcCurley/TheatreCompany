using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheatreCompany.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name ="Category")]
        public string Name { get; set; }

        //========================
        // Navigational Properties
        //========================
        // This is a list of posts that belong to Category
        public List<Post> Posts { get; set; }
    }
}