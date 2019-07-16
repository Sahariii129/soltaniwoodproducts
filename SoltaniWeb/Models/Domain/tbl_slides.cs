using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_slides
    {
        public int slideID { get; set; }
        public string nameslide { get; set; }
        public string nameimage { get; set; }
        public string discription { get; set; }
        public bool? show { get; set; }
        public int? rank { get; set; }
        public bool? first { get; set; }
        public int? ftpid { get; set; }
        public byte[] imge { get; set; }
        public byte[] thumbnail_image { get; set; }

        public virtual tbl_ftp ftp { get; set; }
    }
}
