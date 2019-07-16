using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace SoltaniWeb.Models.Services
{
    

    public class UserServices : IUserServices

    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();

        public tbl_user GetUserByUsername(string username)
        {
            return db.tbl_user.SingleOrDefault(a => a.username == username);
        }

        public int GetUseridByUsername(string username)
        {
            return GetUserByUsername(username).id;
        }

        public bool GiveAccessMenuToUser(int userid)
        {

            foreach (var item in db.tbl_accesstypes)
            {
                if (db.tbl_accesslevels.Where(a => a.userid == userid && a.accessid == item.id).SingleOrDefault() == null)
                {
                    tbl_accesslevels t = new tbl_accesslevels();
                    t.accessid = item.id;
                    t.accessvalue = false;
                    t.userid = userid;
                    db.tbl_accesslevels.Add(t);

                }
            }
            db.SaveChanges();
            // default true
            var q2 = db.tbl_accesslevels.Where(a => a.userid == userid && a.accessid == 2).SingleOrDefault();
            var q3 = db.tbl_accesslevels.Where(a => a.userid == userid && a.accessid == 33).SingleOrDefault();
            q2.accessvalue = true;
            q3.accessvalue = true;
            //
            db.SaveChanges();
            return true;




        }

        public bool updateaccessleveltouser(int userid)
        {
            tbl_user user = db.tbl_user.Find(userid);
            

            foreach (var item in db.tbl_actions)
            {
                var qaccessnew = db.tbl_actionaccessibility.Where(a => a.userid == userid && a.acction_id == item.id).SingleOrDefault();
                if (qaccessnew == null)
                {
                    tbl_actionaccessibility t = new tbl_actionaccessibility();
                    if (item.id == 97 || item.id == 98 || item.id == 27)
                    {


                        t.acction_id = item.id;
                        t.userid = userid;
                        t.permission = true;
                        db.tbl_actionaccessibility.Add(t);
                    }
                    else
                    {
                        t.acction_id = item.id;
                        t.userid = userid;
                        t.permission = false;
                        db.tbl_actionaccessibility.Add(t);
                    }

                };

            }
            db.SaveChanges();

            return true;
        }

        public bool UserCanSeeMoreProductInfo(string username)
        {
            tbl_user u = GetUserByUsername(username);
            bool Flag = false;
            if (u.Access==0 || u.Access==1)
            {
                Flag = true;
            }

            return Flag;
            
        }
    }
}
