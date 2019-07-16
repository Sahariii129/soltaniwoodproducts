using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_contactus
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string text { get; set; }
        public DateTime sabtdate { get; set; }
        public string ip { get; set; }
    }
}
