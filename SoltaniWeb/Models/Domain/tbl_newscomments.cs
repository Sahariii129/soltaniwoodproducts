using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_newscomments
    {
        public int id { get; set; }
        public int newsid { get; set; }
        public DateTime datecomment { get; set; }
        public string ip { get; set; }
        public string text { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool comfirmation { get; set; }
        public int? parentid { get; set; }

        public virtual tbl_news news { get; set; }
    }
}
