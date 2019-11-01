using System;
using System.Collections.Generic;
using System.Text;

namespace scheduler.Model.ViewModel
{
    public class ResponseModel
    {
        public dynamic data { get; set; }
        public string message { get; set; }
        public Status status { get; set; }


    }


    public enum Status
    {
        Error,
        Success,
        Warning
    }
}
