using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_PersonInformationSetting
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }
        public int PersonId { get; set; }
        public virtual tbl_person Person { get; set; }
    }
}
