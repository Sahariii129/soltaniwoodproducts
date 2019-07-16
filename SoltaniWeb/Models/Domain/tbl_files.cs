using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_files
    {
        public  tbl_files()
        {
            tbl_samples = new HashSet<tbl_samples>();
        }

        public int id { get; set; }
        public string filename { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public int userid { get; set; }
        public DateTime date { get; set; }
        public int? madebyid { get; set; }
        public int? decortypeid { get; set; }
        public int? ftpid { get; set; }
        public int? likes { get; set; }
        public int? dislikes { get; set; }
        public int? visits { get; set; }

        public virtual tbl_DecorType decortype { get; set; }
        public virtual tbl_ftp ftp { get; set; }
        public virtual tbl_person madeby { get; set; }
        public virtual tbl_user user { get; set; }
        public virtual ICollection<tbl_samples> tbl_samples { get; set; }
    }
}
