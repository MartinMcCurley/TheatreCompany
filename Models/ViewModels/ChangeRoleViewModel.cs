using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheatreCompany.Models.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; } = string.Empty;
        
        [Display(Name = "User Email")]
        public string UserEmail { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; } = string.Empty;
        
        [Display(Name = "Available Roles")]
        public List<SelectListItem> AllRoles { get; set; } = new List<SelectListItem>();
        
        [Display(Name = "Current Roles")]
        public string[] SelectedRoles { get; set; } = Array.Empty<string>();
    }
}