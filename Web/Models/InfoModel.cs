using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public enum StatusMessage
    {
        Accept = 1, Error, Mail
    }
    public class InfoModel
    {
        public string header { get; set; }
        public string message { get; set; }
        public bool? isMobile { get; set; }
        public StatusMessage status { get; set; }
    }

}