using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public class tbl_GroupPersons
    {
        public int PersonId { get; set; }
        public virtual tbl_person Person { get; set; }
        public int GroupId { get; set; }
        public virtual tbl_Groups Group { get; set; }
    }
}
