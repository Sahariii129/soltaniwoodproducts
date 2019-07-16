using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SoltaniWeb.Controllers
{


    public class ErrorsController : Controller
    {
        [HttpGet]
        public ActionResult Http404()
        {
            Response.StatusCode = 404;
            return View();
        }

        [HttpGet]
        public ActionResult Http503()
        {
            Response.StatusCode = 503;
            return View();
        }


        [HttpGet]
        public ActionResult Http500()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}