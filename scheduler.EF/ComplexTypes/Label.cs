using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace scheduler.EF.ComplexTypes
{
    public class Label
    {
        [Key]
        public int PrimeID { get; set; }
        public string SugErua { get; set; }
        public string Color { get; set; }
    }
}
