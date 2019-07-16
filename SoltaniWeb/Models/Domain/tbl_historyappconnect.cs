using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_historyappconnect
    {
        public int id { get; set; }
        public int? app_id { get; set; }
        public string howtoconnect { get; set; }
        public DateTime? sabtdate { get; set; }
        public int userid { get; set; }
        public string result { get; set; }
        public string time { get; set; }

        public virtual tbl_applicants app_ { get; set; }
        public virtual tbl_user user { get; set; }
    }
}
