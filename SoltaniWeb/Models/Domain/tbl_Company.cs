using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_Company
    {
        public tbl_Company()
        {
            tbl_CompanyPerson = new HashSet<tbl_CompanyPerson>();
            tbl_CompanySection = new HashSet<tbl_CompanySection>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Type { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        [StringLength(11)]
        public string Phone2 { get; set; }

        [StringLength(11)]
        public string Phone3 { get; set; }

        public string Address { get; set; }

        [StringLength(50)]
        public string CompanyRegistrationNumber { get; set; }

        public virtual ICollection<tbl_CompanyPerson> tbl_CompanyPerson { get; set; }

        public virtual ICollection<tbl_CompanySection> tbl_CompanySection { get; set; }
    }
}
