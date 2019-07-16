using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_ftp
    {
        public  tbl_ftp()
        {
            tbl_files = new HashSet<tbl_files>();
            tbl_filestosend = new HashSet<tbl_filestosend>();
            tbl_newsimagefiles = new HashSet<tbl_newsimagefiles>();
            tbl_products = new HashSet<tbl_products>();
            tbl_section = new HashSet<tbl_section>();
            tbl_slides = new HashSet<tbl_slides>();
            tbl_user = new HashSet<tbl_user>();
        }

        public int id { get; set; }
        public string ftpname { get; set; }
        public string serverlink { get; set; }
        public string path1 { get; set; }
        public string path2 { get; set; }
        public string ftpusername { get; set; }
        public string ftppassword { get; set; }
        public int? ftpport { get; set; }

        public virtual ICollection<tbl_files> tbl_files { get; set; }
        public virtual ICollection<tbl_filestosend> tbl_filestosend { get; set; }
        public virtual ICollection<tbl_newsimagefiles> tbl_newsimagefiles { get; set; }
        public virtual ICollection<tbl_products> tbl_products { get; set; }
        public virtual ICollection<tbl_section> tbl_section { get; set; }
        public virtual ICollection<tbl_slides> tbl_slides { get; set; }
        public virtual ICollection<tbl_user> tbl_user { get; set; }
    }
}
