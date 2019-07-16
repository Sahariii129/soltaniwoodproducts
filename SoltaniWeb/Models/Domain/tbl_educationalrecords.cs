using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_educationalrecords
    {
        public int id { get; set; }
        public int applicant_id { get; set; }
        public int? degree_id { get; set; }
        public string Field_Study { get; set; }
        public string university { get; set; }
        public string city { get; set; }
        public decimal? score { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }

        public virtual tbl_applicants applicant_ { get; set; }
        public virtual tbl_educationaldegree degree_ { get; set; }
    }
}
