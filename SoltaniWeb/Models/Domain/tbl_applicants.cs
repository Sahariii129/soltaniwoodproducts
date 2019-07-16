using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_applicants
    {
        public  tbl_applicants()
        {
            tbl_appcontactsinfo = new HashSet<tbl_appcontactsinfo>();
            tbl_applicantrelativeinfo = new HashSet<tbl_applicantrelativeinfo>();
            tbl_educationalrecords = new HashSet<tbl_educationalrecords>();
            tbl_historyappconnect = new HashSet<tbl_historyappconnect>();
            tbl_jobexperiences = new HashSet<tbl_jobexperiences>();
            tbl_skillappinfo = new HashSet<tbl_skillappinfo>();
        }

        public int applicant_id { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }
        public string codemelli { get; set; }
        public string shenasnamenum { get; set; }
        public string fathername { get; set; }
        public string cellnumber { get; set; }
        public int? sex { get; set; }
        public int? degree_id { get; set; }
        public byte[] applicant_image { get; set; }
        public int? job_id { get; set; }
        public bool? Married { get; set; }
        public string borncity { get; set; }
        public DateTime? birthday { get; set; }
        public string Religion { get; set; }
        public bool? Mentalphysicalhealth { get; set; }
        public int? militaryservicestatus { get; set; }
        public bool? Insurance { get; set; }
        public string Insurancepriod { get; set; }
        public string Insurancenumber { get; set; }
        public string method_introduction { get; set; }
        public string Expected_salary { get; set; }
        public bool? workingnow { get; set; }
        public string nationality { get; set; }
        public int? Type_cooperation { get; set; }
        public int? number_child { get; set; }
        public bool? status_smoking { get; set; }
        public DateTime? sabtdate { get; set; }
        public string edu_field { get; set; }
        public bool? driverlicense { get; set; }
        public long? uniqekey { get; set; }
        public bool? visible { get; set; }
        public int? finalscore { get; set; }

        public virtual tbl_educationaldegree degree_ { get; set; }
        public virtual tbl_job job_ { get; set; }
        public virtual ICollection<tbl_appcontactsinfo> tbl_appcontactsinfo { get; set; }
        public virtual ICollection<tbl_applicantrelativeinfo> tbl_applicantrelativeinfo { get; set; }
        public virtual ICollection<tbl_educationalrecords> tbl_educationalrecords { get; set; }
        public virtual ICollection<tbl_historyappconnect> tbl_historyappconnect { get; set; }
        public virtual ICollection<tbl_jobexperiences> tbl_jobexperiences { get; set; }
        public virtual ICollection<tbl_skillappinfo> tbl_skillappinfo { get; set; }
    }
}
