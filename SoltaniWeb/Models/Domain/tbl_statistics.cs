using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_statistics
    {
        public int id { get; set; }
        public string controllername { get; set; }
        public string actionname { get; set; }
        public string ip { get; set; }
        public int? user_id { get; set; }
        public DateTime enterdate { get; set; }
        public string parameter { get; set; }
        public string fullurl { get; set; }

        public virtual tbl_user user_ { get; set; }
    }
}
