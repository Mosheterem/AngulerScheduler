using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.EF.Model
{
    public partial class UserDetail
    {
        //public UserDetail()
        //{
        //    UserTempCards = new HashSet<UserTempCards>();
        //}

        public int Id { get; set; }
        public string AssociationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
        public string BarCode { get; set; }
        public string Rfid { get; set; }
        public byte RoleId { get; set; }
        public short DesignationId { get; set; }
        public string ImageName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

       // public virtual Designations Designation { get; set; }
        public virtual Logins IdNavigation { get; set; }
       // public virtual Roles Role { get; set; }
       // public virtual ICollection<UserTempCards> UserTempCards { get; set; }
    }
}
