using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_filestosend
    {
        public int id { get; set; }
        public string caption { get; set; }
        public string imagename { get; set; }
        public int? ftp_id { get; set; }
        public int? user_id { get; set; }
        public DateTime? uploaddatetime { get; set; }
        public byte[] filecontent { get; set; }

        public virtual tbl_ftp ftp_ { get; set; }
        public virtual tbl_user user_ { get; set; }
    }
}
