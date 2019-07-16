using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;
namespace SoltaniWeb.Models.Authorize_swp
{
    public static class Authorize_swp
    {
        public static bool thisuserallowtodo(int userid = 0, int accessid = 0)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            var record = db.tbl_accesslevels.Where(a => a.accessid == accessid && a.userid == userid && a.accessvalue == true);
            if (record.Count() > 0)
            {

                return true;
            }
            else
            {
                return false;
            }
        }


    }
}