using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.structs.ArchiveSmsVM
{
    public class ArchiveSmsCreateViewModel
    {
        public int UserId { get; set; }
        public string Context { get; set; }
        public List<int> PersonsId { get; set; }

    }
}
