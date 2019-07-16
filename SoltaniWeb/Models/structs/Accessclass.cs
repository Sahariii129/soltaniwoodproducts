using SoltaniWeb.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.structs
{
    public class Accessclass
    {
        public string user { get; set; }
        public IEnumerable<tbl_actionaccessibility> actionaccesslist { get; set; }
    }

    public class MenuViewModel
    {
        public int menuid { get; set; }
        public int userid { get; set; }
        public string menuname { get; set; }
        public string menunameinEN { get; set; }
    }

    public class ActionAccessModelView
    {
        public int id { get; set; }
        public string actionname { get; set; }
        public int actionid { get; set; }
        public bool permission { get; set; }
        public int userid { get; set; }
        public int menuid { get; set; }
    }
}
