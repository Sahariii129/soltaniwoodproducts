using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SoltaniWeb.Models.Domain;

namespace SoltaniWeb.Controllers
{
    public class GroupPersonController : Controller
    {
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
        public ActionResult FilterMenuCustomization_Groups()
        {
            var result = db.tbl_Groups.Select(x => x.Name).ToList();
            return Json(result);
        }
    }
}