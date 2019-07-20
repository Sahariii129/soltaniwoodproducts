using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.structs.CompanyVM
{
    public class CompanySectionViewModel
    {
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public int Id { get; set; }
        public string SectionName { get; set; }
        public string CompanySectionDescription { get; set; }
        public string SectionKeywords { get; set; }
        public string SectionImage { get; set; }
        public string SectionShortName { get; set; }
        public bool? SectionStatus { get; set; }
        public int? SectionOrdering { get; set; }
        public string SectionNameEn { get; set; }
    }
}
