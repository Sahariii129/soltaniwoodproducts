using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoltaniWeb.Models.Domain
{
    public partial class tbl_sentMessag
    {
        public tbl_sentMessag()
        {
            SentMessagPersones = new HashSet<tbl_SentMessagPerson>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string ContextMessage { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string State { get; set; }
        public string RefNumber { get; set; }
        public virtual tbl_user User { get; set; }
        public virtual ICollection<tbl_SentMessagPerson> SentMessagPersones { get; set; }
    }
}
