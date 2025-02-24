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

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        //========================
        // Navigational Properties
        //========================
        // This is a list of posts that belong to Category
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}