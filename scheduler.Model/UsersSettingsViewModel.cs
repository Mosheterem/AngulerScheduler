using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Model
{
    public class OldUserSettings
    {
       
        public int PrimeId { get; set; }
        public string ClientId { get; set; }
        public int UserId { get; set; }
        public string Resoucresgroup { get; set; }
        public bool IsCrud { get; set; }
        public bool IsShowDeletedRecords { get; set; }
        public bool StartViewAsMerged { get; set; }
        public string StartHour { get; set; }
        public string EndHour { get; set; }
        public string CellDuration { get; set; }
        public DateTime LastUserLogin { get; set; }
        public DateTime LastDBUpdated { get; set; }
        public string primeidName { get; set; }
        public String ResourceName { get; set; }
        public String userName { get; set; }

        public string eMail { get; set; }
        public bool crudAllow { get; set; }
        public string Password { get; set; }
    }
    public class UsersSettingsViewModel
    {
        public OldUserSettings Settings { get; set; }
        public List<TempDataSettings> owners { get; set; }
    }

    public class reportsendPayload
    {
        public string email { get; set; }
        public string body { get; set; }
    }
    public class TempDataSettings
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public bool IsCheked { get; set; }
    }
}
