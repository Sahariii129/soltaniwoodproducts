using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Controllers
{
    public class BrancheController : Controller
    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        public IActionResult Index()
        {
            return
            View();
        }
        public ActionResult FilterMenuCustomization_Branches()
        {
            var result = db.tbl_branches.Select(x => x.branch_name).ToList();
            return Json(result);
        }
    }
}