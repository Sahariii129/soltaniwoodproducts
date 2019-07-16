using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_onlinechatmsg
    {
        public int id { get; set; }
        public int onlineuser_id { get; set; }
        public string textmsg { get; set; }
        public DateTime sendmsg_date { get; set; }
        public int? supporter_id { get; set; }
        public int? towhom { get; set; }

        public virtual tbl_tbl_onlineusers onlineuser_ { get; set; }
        public virtual tbl_tbl_onlineusers supporter_ { get; set; }
        public virtual tbl_tbl_onlineusers towhomNavigation { get; set; }
    }
}
