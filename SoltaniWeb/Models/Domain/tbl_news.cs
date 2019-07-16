using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_news
    {
        public  tbl_news()
        {
            tbl_newscomments = new HashSet<tbl_newscomments>();
            tbl_newsimages = new HashSet<tbl_newsimages>();
        }

        public int id { get; set; }
        public int? newtypeid { get; set; }
        public string title { get; set; }
        public string abstractnews { get; set; }
        public string fulltext { get; set; }
        public DateTime writedate { get; set; }
        public DateTime issudate { get; set; }
        public int? likes { get; set; }
        public int? dislikes { get; set; }
        public int? visits { get; set; }
        public int? userid { get; set; }
        public string imagefilename { get; set; }
        public bool? confirmation { get; set; }
        public string keywords { get; set; }

        public virtual tbl_newstype newtype { get; set; }
        public virtual tbl_user user { get; set; }
        public virtual ICollection<tbl_newscomments> tbl_newscomments { get; set; }
        public virtual ICollection<tbl_newsimages> tbl_newsimages { get; set; }
    }
}
