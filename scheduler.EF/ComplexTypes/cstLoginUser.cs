using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.EF.ComplexTypes
{
    public class cstLoginUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<DateTime> LastLogin { get; set; }
        public bool PasswordExpired { get; set; }
        public int DaystoExpire { get; set; }
        public short DesignationId { get; set; }
        public string DesignationName { get; set; }
        public byte RoleId { get; set; }
        public string RoleName { get; set; }
        public bool ResetPassword { get; set; }
        public string ImageName { get; set; }
        public string UserName { get; set; }
    }
}
