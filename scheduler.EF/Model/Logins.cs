using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.EF.Model
{
    public partial class Logins
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public bool Active { get; set; }
        public DateTime PasswordExpiresOn { get; set; }
        public bool ResetPassword { get; set; }
        public DateTime? LastLogin { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual UserDetail UserDetail { get; set; }
    }
}
