using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace scheduler.EF.ComplexTypes
{
    public partial class TASK
    {
        [Key]
        // public int id { get; set; }
        public Nullable<int> PrimeID { get; set; }
        public string recurrenceRule { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<bool> AllDayEvent { get; set; }

        public Nullable<bool> allDay { get; set; }
        public string Teur { get; set; }
        public string OwnerIds { get; set; }
        public int Label { get; set; }

        public string Remark { get; set; }
        public string LtName { get; set; }
        public string WITHWHO { get; set; }
        public string TIK { get; set; }
        public string PLACE { get; set; }
        public string LakNum { get; set; }

        public string withwho { get; set; }
        public string lakNum { get; set; }
        // public object recurrenceRule { get; set; }allDay
        public string attendees { get; set; }
      
        public Nullable<System.DateTime> DATE_OUT { get; set; }
         public Nullable<bool> IsDeleted { get; set; }
         public string HexBackColor { get; set; }
         public string HexForeColor { get; set; }
        public string HexBarColor { get; set; }

    }
}
