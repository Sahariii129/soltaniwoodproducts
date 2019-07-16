using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Models
{
    public static class statistics


    {
        public static bool statisticssave(string controller, string action, string ip, Nullable<int> userid)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();
            tbl_statistics t = new tbl_statistics();
            t.actionname = action;
            t.controllername = controller;
            t.enterdate = DateTime.Now;
            t.ip = ip;
            if (userid != null)
            {
                t.user_id = userid;
            }

            db.tbl_statistics.Add(t);
            db.SaveChanges();
            return true;



        }
        


    }
}