using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_newsimagefiles
    {
        public  tbl_newsimagefiles()
        {
            tbl_newsimages = new HashSet<tbl_newsimages>();
        }

        public int id { get; set; }
        public string filename { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public DateTime date { get; set; }
        public int? ftpid { get; set; }
        public int? userid { get; set; }

        public virtual tbl_ftp ftp { get; set; }
        public virtual tbl_user user { get; set; }
        public virtual ICollection<tbl_newsimages> tbl_newsimages { get; set; }
    }
}
