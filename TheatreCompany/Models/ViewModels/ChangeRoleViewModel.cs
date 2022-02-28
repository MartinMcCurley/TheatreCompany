// Author: Martin McCurley | Date: 16/02/22
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheatreCompany.Models.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserName { get; set; }
        public string OldRole { get; set; }
        public ICollection<SelectListItem> Roles { get; set; }

        [Required, Display(Name = "Role")]
        public string Role { get; set; }

    }
}