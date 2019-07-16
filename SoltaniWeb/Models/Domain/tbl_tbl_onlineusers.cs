using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_tbl_onlineusers
    {
        public  tbl_tbl_onlineusers()
        {
            tbl_onlinechatmsgonlineuser_ = new HashSet<tbl_onlinechatmsg>();
            tbl_onlinechatmsgsupporter_ = new HashSet<tbl_onlinechatmsg>();
            tbl_onlinechatmsgtowhomNavigation = new HashSet<tbl_onlinechatmsg>();
        }

        public int id { get; set; }
        public string connection_id { get; set; }
        public string nameu { get; set; }
        public int? user_id { get; set; }
        public string email { get; set; }

        public virtual tbl_user user_ { get; set; }
        public virtual ICollection<tbl_onlinechatmsg> tbl_onlinechatmsgonlineuser_ { get; set; }
        public virtual ICollection<tbl_onlinechatmsg> tbl_onlinechatmsgsupporter_ { get; set; }
        public virtual ICollection<tbl_onlinechatmsg> tbl_onlinechatmsgtowhomNavigation { get; set; }
    }
}
