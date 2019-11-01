using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace scheduler.Model.ViewModel
{
   public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }
        //[Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
