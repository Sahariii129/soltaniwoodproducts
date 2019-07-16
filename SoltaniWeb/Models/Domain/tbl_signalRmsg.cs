using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_signalRmsg
    {
        public int id { get; set; }
        public int from_userid { get; set; }
        public int? to_userid { get; set; }
        public string msg_text { get; set; }
        public byte[] files { get; set; }
        public DateTime? datetime_send { get; set; }
        public DateTime? datetime_read { get; set; }
        public bool? visibleforall { get; set; }
        public bool? visibletofrom { get; set; }
        public bool? visibletoto { get; set; }
        public bool? pin { get; set; }
        public DateTime? datetime_pin { get; set; }
        public string topic_pin { get; set; }
        public int? msgcondition { get; set; }
        public string msg_textpure { get; set; }

        public virtual tbl_user from_user { get; set; }
        public virtual tbl_user to_user { get; set; }
    }
}
