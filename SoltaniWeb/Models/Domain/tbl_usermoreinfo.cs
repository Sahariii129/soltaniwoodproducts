using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_usermoreinfo
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string fathername { get; set; }
        public string home_address { get; set; }
        public string work_address { get; set; }
        public string home_tell { get; set; }
        public string work_tell { get; set; }
        public string cellphone1 { get; set; }
        public string cellphone2 { get; set; }
        public string job { get; set; }
        public DateTime? birthday { get; set; }
        public int? sex { get; set; }
        public string codemelli { get; set; }

        public virtual tbl_user user_ { get; set; }
    }
}
