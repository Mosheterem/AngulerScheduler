using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace scheduler.EF.Model
{
    public class CaltUsers
    {
        [Key]
        public int PrimeID { get; set; }
        public string ClientID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string eMail { get; set; }

        public string ResourceName { get; set; }
        public string ConnectionString { get; set; }
        public DateTime LastUserLogin { get; set; }
        public DateTime LastDBUpdated { get; set; }
        public string MacAdress { get; set; }
        public string CPUID { get; set; }
        public string RestorEmail { get; set; }

        [NotMapped]
        public string PrimeIDName { get; set; }
    }
}
