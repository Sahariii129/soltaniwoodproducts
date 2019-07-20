using System;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SoltaniWeb.Models.Domain;
using SoltaniWeb.Models.Extensions;
using SoltaniWeb.Models.structs.CompanyVM;
using SoltaniWeb.Models.Services.Company;
using SoltaniWeb.Models.Services.Person;

namespace SoltaniWeb.Controllers
{
    [SWPAuthorizeIdentityController]

    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        _4820_soltaniwebContext db = new _4820_soltaniwebContext();
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        public IActionResult Index()
        {
            return
            View();
        }

        public ActionResult Company_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = _companyService.GetAll();
            var dsResultTemps = data.ToDataSourceResult(request);
            return Json(dsResultTemps);
        }

        public IActionResult Create(int? id)
        {
            var company = new CompanyViewModel();
            if (id != null)
            {
                company = _companyService.FindCompany(id.Value);
            }
            return View(company);
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
        {
            
            ResultStatus op = new ResultStatus();
            
            try
            {
                if (ModelState.IsValid)
                {
                    model.CompanyType = model.CompanyType.ToEnum<CompanyType>().ToString();
                    if (model.Id != 0)
                    {
                        _companyService.Update(model);
                        op.Message = "عملیات درخواستی با موفقیت انجام شد";
                        op.IsSuccessed = true;
                    }
                    else
                    {
                        var companyId= _companyService.Create(model);
                        op.Message = companyId == 0 ? "خطا در عملیات" : "عملیات درخواستی با موفقیت انجام شد";
                        op.IsSuccessed = companyId != 0 ;
                    }
                   
                    
                    return Json(new { ResultStatus = op });
                }
                op.IsSuccessed = true;
                op.Message = "اطلاعات را کامل کنید";
                return Json(new { ResultStatus = op });
            }
            catch (Exception e)
            {
                op.IsSuccessed = false;
                op.Message = "خطا در عملیات";
                return Json(new { ResultStatus = op });
            }
           
        }
    }
}