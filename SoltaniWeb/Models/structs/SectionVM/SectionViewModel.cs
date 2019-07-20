using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.structs.SectionVM
{
    public class SectionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Keywords { get; set; }
        public string Shortname { get; set; }
        public bool? Status { get; set; }
    }
}
