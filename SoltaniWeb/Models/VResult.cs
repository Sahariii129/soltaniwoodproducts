using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoltaniWeb.Models

{
     public class VerifyResult
    {
        public bool success { get; set; }
        public String TransActionID { get; set; }
        public String Amount { get; set; }
        public String SuccessMessage { get; set; }
        public bool error { get; set; }
        public String ErrorMessage { get; set; }
    }
}