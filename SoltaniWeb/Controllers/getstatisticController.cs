using Microsoft.AspNetCore.Mvc.Filters;
using SoltaniWeb.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace SoltaniWeb.Controllers
{
    public class getstatisticController : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _4820_soltaniwebContext db = new _4820_soltaniwebContext();

            if (filterContext.HttpContext.Session.GetInt32("userid") == null)
            {

                if (filterContext.HttpContext.Request.Cookies["userid"] != null)
                {
                    filterContext.HttpContext.Session.SetString("username", filterContext.HttpContext.Request.Cookies["username"]);
                    filterContext.HttpContext.Session.SetInt32("userid", int.Parse(filterContext.HttpContext.Request.Cookies["userid"]));
                   
                    int userid = filterContext.HttpContext.Session.GetInt32("userid").Value;
                    string fname = db.tbl_user.Where(a => a.id == userid).SingleOrDefault().name;
                    string lname = db.tbl_user.Where(a => a.id == userid).SingleOrDefault().Lname;
                    string fullname = fname + " " + lname;
                    filterContext.HttpContext.Session.SetString("fullname",fullname);

                }
            }



            tbl_statistics t = new tbl_statistics();

            //t.actionname = (string)routingValues["action"];
            t.actionname = filterContext.RouteData.Values["Action"].ToString();
            t.controllername = filterContext.RouteData.Values["Controller"].ToString();
            t.enterdate = DateTime.Now;
            t.ip = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();
            if (filterContext.HttpContext.Session.GetInt32("userid") == null)
            {
                t.user_id = null;
            }
            else
            {

                t.user_id = filterContext.HttpContext.Session.GetInt32("userid");
            }

            //var myparams = filterContext.ActionParameters;
            var myparams = filterContext.ActionArguments;
            string parameters = "";
            int x = 0;
            int count = 0;
            count = myparams.Count();


            foreach (var item in myparams)
            {
                if (count == 1 || x == count - 1)
                {
                    if (item.Value == null)
                    {

                        parameters = parameters + item.Key + "=null";
                    }
                    else
                    {
                        parameters = parameters + item.Key + "=" + item.Value.ToString();

                    }
                }
                else
                {

                    if (item.Value == null)
                    {

                        parameters = parameters + item.Key + "=null" + "&";
                    }
                    else
                    {
                        parameters = parameters + item.Key + "=" + item.Value.ToString() + "&";

                    }


                }
                x += 1;

            }



            t.parameter = parameters;
            t.fullurl = "www.soltaniwoodproducts.com/" + t.controllername + "/" + t.actionname + "?" + parameters;
            db.tbl_statistics.Add(t);
            db.SaveChanges();
        }


    }


   


}