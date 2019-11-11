using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Model.ViewModel
{
   public class FeedBack
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public string FullName { get; set; }
        public Boolean Seen { get; set; }
        public DateTime ResponseKey { get; set; }
    }
}
