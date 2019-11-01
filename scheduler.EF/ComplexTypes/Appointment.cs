using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.EF.ComplexTypes
{
    public class Appointment
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public List<int> ownerIds { get; set; }
        public string teur { get; set; }
        public string ltName { get; set; }
        public int label { get; set; }
        public string remark { get; set; }
        public string lakNum { get; set; }
        public string tiK { get; set; }
        public string withwho { get; set; }
        public string place { get; set; }
        public string description { get; set; }
        public string Text { get; set; }
        public string HexBackColor { get; set; }
        public string HexForeColor { get; set; }
        public string HexBarColor { get; set; }
        public bool AllDayEvent { get; set; }
        
    }
}
