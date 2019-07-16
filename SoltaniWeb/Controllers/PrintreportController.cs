using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoltaniWeb.Hubs;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SoltaniWeb.Controllers
{
    public class PrintreportController : Controller
    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();

        private readonly IHubContext<ChatHub> _hub;
        private IHostingEnvironment _hostingEnvironment;


        public PrintreportController(IHubContext<ChatHub> hub, IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
            _hub = hub;
           
        }




        public IActionResult Index()
        {
            return View();
        }




        public IActionResult orderreport2()
        {
            return View();

        }
        public IActionResult orderreport3()
        {
            return View();

        }
        public ActionResult GetReportoforder(int orderid = 0)
        {
            var order = db.tbl_order.ToList().Where(a => a.id == orderid).Select(g => new
            {
                id = g.id.ToString(),
                username = g.user.username,
                sodoordate = g.sodoor_date.ToPersianDate(),
                sodoortime = g.sodoor_date.ToShortTimeString(),
                fullname = g.orderfor.Fname + " " + g.orderfor.Lname,
                cellphone = g.orderfor.cell,
                frombranch = g.from_branch.branch_name,
                tobranch = g.to_branch.branch_name,
                sharh = g.sharh,
                done = (g.done == false ? "نزد " + g.to_branch.branch_name : "تحویل شده"),
                address = g.orderfor.address,
                frombranch_address = g.from_branch.Address,
                tobranch_address = g.to_branch.Address
            });


            var orderitems = db.tbl_orderdetails.Where(a => a.order_id == orderid).ToList().Select(g => new
            {
                productid = g.product_id.ToString(),
                Catgname = g.product_.category.categoryname,
                productname = g.product_.category.categoryname + "| " + g.product_.name + "| " + g.product_.codename + "| " + g.product_.dimension,
                weight = g.product_.weight.ToString() + "kg",
                number = g.number.ToString(),
                totalweight = (g.product_.weight.HasValue ? (g.product_.weight.Value * g.number).ToString() : ""),
                desc = g.description



            });


            return Json(new { order = order, orderitems= orderitems });
        }



    }
}