using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JazAndSeanSATProject.UI.MVC.Models
{
    public class ContactViewModel
    {

        [Required(ErrorMessage = "* Your Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Your email is required")]
        [EmailAddress]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "* A message is required")]
        [UIHint("MultilineText")]
        public string Message { get; set; }


    }//end class
}//end namespace