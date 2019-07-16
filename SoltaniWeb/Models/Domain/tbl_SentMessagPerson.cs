using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public class tbl_SentMessagPerson
    {
        public int Id { get; set; }
        public int SentMessagId { get; set; }
        public int? PersonId { get; set; }
        public string PersonCellPhone { get; set; }
        public string DeliveryStatus { get; set; }
        public virtual tbl_person Person { get; set; }
        public virtual tbl_sentMessag SentMessag { get; set; }
    }
}
