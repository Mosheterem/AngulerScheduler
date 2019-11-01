using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace scheduler.Model.ViewModel
{
    public class SetPasswordModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }
    }
}
