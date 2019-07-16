using System;
using System.Collections.Generic;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_person
    {
        public  tbl_person()
        {
            tbl_Pcontact = new HashSet<tbl_Pcontact>();
            tbl_files = new HashSet<tbl_files>();
            tbl_order = new HashSet<tbl_order>();
            tbl_SentMessagPerson = new HashSet<tbl_SentMessagPerson>();
            Groups = new HashSet<tbl_GroupPersons>();
            PersonInformationSettings = new HashSet<tbl_PersonInformationSetting>();
            PersonAddresses = new HashSet<tbl_PersonAddress>();
            tbl_CompanyPerson = new HashSet<tbl_CompanyPerson>();
        }

        public int id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string prefix { get; set; }
        public string sex { get; set; }
        public string codemelli { get; set; }
        public DateTime? birthday { get; set; }
        public string Pdescription { get; set; }
        public string job { get; set; }
        public string tell { get; set; }
        public string cell { get; set; }
        public string address { get; set; }
        public int BrancheId { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public virtual tbl_branches Branches { get; set; }
        public virtual ICollection<tbl_CompanyPerson> tbl_CompanyPerson { get; set; }
        public virtual ICollection<tbl_Pcontact> tbl_Pcontact { get; set; }
        public virtual ICollection<tbl_files> tbl_files { get; set; }
        public virtual ICollection<tbl_order> tbl_order { get; set; }
        public virtual ICollection<tbl_SentMessagPerson> tbl_SentMessagPerson { get; set; }
        public virtual ICollection<tbl_GroupPersons> Groups { get; set; }
        public virtual ICollection<tbl_PersonInformationSetting> PersonInformationSettings { get; set; }

        public virtual ICollection<tbl_PersonAddress> PersonAddresses { get; set; }
    }
}
