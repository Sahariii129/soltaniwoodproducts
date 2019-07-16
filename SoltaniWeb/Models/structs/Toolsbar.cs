using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.structs
{
    public class Toolsbar
    {
        public bool? Add { get; set; }
        public bool? Edit { get; set; }
        public bool? Delete { get; set; }

        public bool? print { get; set; }

        public bool? Search { get; set; }

        public bool? Navigation { get; set; }

        public bool? Refresh { get; set; }
    }
}
