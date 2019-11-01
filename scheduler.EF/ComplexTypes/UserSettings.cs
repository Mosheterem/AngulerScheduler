using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace scheduler.EF.ComplexTypes
{
   public class UserSettings
    {
        [Key]
        public int PrimeID { get; set; }
        public Nullable<int> PrimeID97 { get; set; }
        public string ClientID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Resoucresgroup { get; set; }
        public string eMail { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public Nullable<bool> IsCrud { get; set; }
        public Nullable<bool> IsShowDeletedRecords { get; set; }
        public Nullable<bool> StartViewAsMerged { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public Nullable<int> CellDuration { get; set; }
        public Nullable<System.DateTime> LastUserLogin { get; set; }
        public Nullable<System.DateTime> RowDateCreated { get; set; }
        public Nullable<System.DateTime> RowDateUpdated { get; set; }
        public string RowCreatedBy { get; set; }
        public string RowUpdatedBy { get; set; }
    }
}
