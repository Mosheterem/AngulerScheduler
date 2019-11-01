using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace scheduler.EF.ComplexTypes
{
    public partial class Resources
    {
        [Key]
        public int UniqueID { get; set; }
        public int ResourceID { get; set; }
        public string ResourceName { get; set; }
        public string Color { get; set; }
        // public byte[] Image { get; set; }
        public bool IsCheked { get; set; }
    }
}
