using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_PersonAddress
    {
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        [StringLength(11)]
        public string Phone2 { get; set; }

        [StringLength(11)]
        public string Phone3 { get; set; }

        [StringLength(11)]
        public string PostalCode { get; set; }

        public int PersonId { get; set; }

        public virtual tbl_person tbl_person { get; set; }
    }
}
