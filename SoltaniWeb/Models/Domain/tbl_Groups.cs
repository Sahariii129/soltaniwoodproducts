using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public class tbl_Groups
    {
        public tbl_Groups()
        {
            Persons = new HashSet<tbl_GroupPersons>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<tbl_GroupPersons> Persons { get; set; }
    }
}
