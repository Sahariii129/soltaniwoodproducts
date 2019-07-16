using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_newsimages
    {
        public int id { get; set; }
        public int? newsid { get; set; }
        public int? newsimageid { get; set; }
        public int? userid { get; set; }

        public virtual tbl_news news { get; set; }
        public virtual tbl_newsimagefiles newsimage { get; set; }
        public virtual tbl_user user { get; set; }
    }
}
